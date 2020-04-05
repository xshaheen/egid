using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EGID.Core.Common.Result;
using EGID.Core.Exceptions;
using EGID.Data.Cards.Dto;
using EGID.Infrastructure.KeysGenerator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EGID.Data.Cards
{
    public class CardService : ICardService
    {
        private readonly IConfiguration _configuration;
        private readonly EgidDbContext _context;
        private readonly UserManager<Card> _userManager;
        private readonly SignInManager<Card> _signInManager;
        private readonly IPasswordValidator<Card> _passwordValidator;
        private readonly IKeyGeneratorService _keyGenerator;

        #region Constructor

        public CardService(
            IConfiguration configuration,
            EgidDbContext context,
            UserManager<Card> userManager,
            SignInManager<Card> signInManager,
            IPasswordValidator<Card> passwordValidator,
            IKeyGeneratorService keyGenerator)
        {
            _configuration = configuration;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordValidator = passwordValidator;
            _keyGenerator = keyGenerator;
        }

        #endregion Constructor

        #region Get

        public async Task<Card> GetAsync(Guid id)
        {
            var card = await _userManager.FindByIdAsync(id.ToString());

            if (card == null)
                throw new EntityNotFoundException("Can not find this card.");

            return card;
        }

        public async Task<Card> GetAsync(string privateKeyXml)
        {
            // TODO: encrypt private keys
            var card = await _userManager.Users
                .FirstOrDefaultAsync(c => c.PrivateKeyXml == privateKeyXml);

            if (card == null)
                throw new EntityNotFoundException("Can not find this card.");

            return card;
        }

        #endregion Get

        #region Active

        public async Task<bool> IsActiveAsync(Guid id)
        {
            var card = await GetAsync(id);

            return card.Active;
        }

        #endregion Active

        #region SignIn

        public async Task<DataResult<string>> SignInAsync(SignInDto model)
        {
            // Is the card exist?
            Card card;
            try
            {
                card = await GetAsync(model.PrivateKey);
            }
            catch (EntityNotFoundException)
            {
                return DataResult<string>.Failure("محاولة تسجيل دخول غير صحيحة.");
            }

            // Is card active?
            if (!card.Active)
                return DataResult<string>.Failure("هذه البطاقة معطلة.");

            // Is card valid?
            if (card.TerminationDate < DateTime.UtcNow)
                return DataResult<string>.Failure("هذه البطاقة منتهية الصلاحية.");

            // Is card locked out
            if (await _userManager.IsLockedOutAsync(card))
                return DataResult<string>.Failure("لقد حاولت عدة مرات تم تعطيل الحساب لبضع دقائق حاول في وقت لاحق.");

            // Is this a correct PIN?

            // TODO: hash this value then compare it
            var isCorrectPin = card.Pin1 == model.Pin1;

            if (isCorrectPin) return DataResult<string>.Success(GenerateJwtToken(card));

            // try to signin with invalid password to increase lockout on failure counter
            // to enabled lockout account if failed to sign a lot of time
            await _signInManager.PasswordSignInAsync(card, "123", false, true);

            return DataResult<string>.Failure("محاولة تسجيل دخول غير صحيحة.");
        }

        #endregion SignIn

        #region Register

        public async Task<DataResult<string>> RegisterAsync(CreateCardDto model)
        {
            // TODO: hash Pin1 & Pin2 first
            // TODO: encrypt private keys
            var card = new Card
            {
                PublicKeyXml = _keyGenerator.PublicKeyXml,
                PrivateKeyXml = _keyGenerator.PrivateKeyXml,
                OwnerId = model.OwnerId,
                Email = model.Email,
                PhoneNumber = model.Phone,
                Pin1 = model.Pin1,
                Pin2 = model.Pin2,
                Active = true,
                IssueDate = DateTime.UtcNow.AddHours(-2),
                TerminationDate = DateTime.UtcNow.AddYears(4)
            };

            var result = await _userManager.CreateAsync(card, model.Puk);

            return result.Succeeded
                ? DataResult<string>.Success(card.Id)
                : DataResult<string>.Failure(result.Errors.Select(e => e.Description));
        }

        #endregion Register

        #region Change Passwords

        public async Task<Result> ChangePin1Async(ChangePin1Dto model)
        {
            var card = await GetAsync(model.CardId);

            // verify Puk
            var signInResult = await _signInManager
                .CheckPasswordSignInAsync(card, model.Puk, true);

            if (!signInResult.Succeeded)
            {
                if (!signInResult.IsLockedOut)
                    return Result.Failure("رمز Puk غير صحيح.");

                return Result.Failure("لقد حاولت عدة مرات تم تعطيل الحساب لبضع دقائق حاول في وقت لاحق.");
            }

            // validate new pin1

            var result = await _passwordValidator.ValidateAsync(_userManager, card, model.NewPin1);

            if (!result.Succeeded)
                return Result.Failure(result.Errors.Select(e => e.Description));

            // change

            // TODO: hash first
            card.Pin1 = model.NewPin1;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Result.Failure(
                    "حدث خطأ لم يتم حفظ التغيرات حاول مرة اخري ان استمر الخطأ من فضلك قم بالابلاغ عنه.");
            }

            return Result.Success();
        }

        public async Task<Result> ChangePin2Async(ChangePin2Dto model)
        {
            var card = await GetAsync(model.CardId);

            // verify Puk
            var signInResult = await _signInManager
                .CheckPasswordSignInAsync(card, model.Puk, true);

            if (!signInResult.Succeeded)
            {
                if (!signInResult.IsLockedOut)
                    return Result.Failure("رمز Puk غير صحيح.");

                return Result.Failure("لقد حاولت عدة مرات تم تعطيل الحساب لبضع دقائق حاول في وقت لاحق.");
            }

            // validate new pin2

            var result = await _passwordValidator.ValidateAsync(_userManager, card, model.NewPin2);

            if (!result.Succeeded)
                return Result.Failure(result.Errors.Select(e => e.Description));

            // change

            // TODO: hash first
            card.Pin2 = model.NewPin2;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Result.Failure(
                    "حدث خطأ لم يتم حفظ التغيرات حاول مرة اخري ان استمر الخطأ من فضلك قم بالابلاغ عنه.");
            }

            return Result.Success();
        }

        public async Task<Result> ChangePukAsync(ChangePukDto model)
        {
            var card = await GetAsync(model.CardId);

            // check provided current PUK

            var checkPasswordResult = await _signInManager
                .CheckPasswordSignInAsync(card, model.CurrentPuk, true);

            if (!checkPasswordResult.Succeeded)
            {
                if (!checkPasswordResult.IsLockedOut)
                    return Result.Failure("رمز Puk غير صحيح.");

                return Result.Failure("لقد حاولت عدة مرات تم تعطيل الحساب لبضع دقائق حاول في وقت لاحق.");
            }

            // validate new PUK

            var validPass = await _passwordValidator
                .ValidateAsync(_userManager, card, model.NewPuk);

            if (!validPass.Succeeded)
                return Result.Failure(validPass.Errors.Select(e => e.Description));

            // change

            var result = await _userManager.ChangePasswordAsync(card, model.CurrentPuk, model.NewPuk);

            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }

        #endregion Change Passwords

        #region Change Active State

        public async Task<Result> InactivateAsync(Guid id)
        {
            var card = await GetAsync(id);

            card.Inactivate();

            _context.Update(card);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Result.Failure(
                    "حدث خطأ لم يتم حفظ التغيرات حاول مرة اخري ان استمر الخطأ من فضلك قم بالابلاغ عنه.");
            }

            return Result.Success();
        }

        public async Task<Result> ActivateAsync(Guid id)
        {
            var card = await GetAsync(id);

            card.Activate();

            _context.Update(card);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Result.Failure(
                    "حدث خطأ لم يتم حفظ التغيرات حاول مرة اخري ان استمر الخطأ من فضلك قم بالابلاغ عنه.");
            }

            return Result.Success();
        }

        #endregion Change Active State

        #region Helper Methods

        private string GenerateJwtToken(Card card)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, card.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            // Expires
            var minutes = Convert.ToDouble(_configuration["JwtExpireMinutes"]);
            var expires = DateTime.Now.AddMinutes(minutes);

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: cred
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion Helper Methods
    }
}

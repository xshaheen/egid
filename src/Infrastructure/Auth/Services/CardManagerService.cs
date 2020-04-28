using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EGID.Common.Exceptions;
using EGID.Common.Interfaces;
using EGID.Common.Models.Result;
using EGID.Infrastructure.Auth.Models;
using EGID.Infrastructure.Auth.Services.Dto;
using EGID.Infrastructure.Common;
using EGID.Infrastructure.Crypto;
using EGID.Infrastructure.KeysGenerator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EGID.Infrastructure.Auth.Services
{
    public class CardService : ICardManagerService
    {
        private readonly IConfiguration _configuration;
        private readonly AuthDbContext _context;
        private readonly UserManager<Card> _userManager;
        private readonly SignInManager<Card> _signInManager;
        private readonly IPasswordValidator<Card> _passwordValidator;
        private readonly PrivateKeyOptions _privateKeyOptions;
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUser;
        private readonly IKeyGeneratorService _keyGenerator;

        #region Constructor

        public CardService(
            IConfiguration configuration,
            AuthDbContext context,
            UserManager<Card> userManager,
            SignInManager<Card> signInManager,
            IPasswordValidator<Card> passwordValidator,
            IKeyGeneratorService keyGenerator,
            PrivateKeyOptions privateKeyOptions,
            IDateTime dateTime,
            ICurrentUserService currentUser)
        {
            _configuration = configuration;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordValidator = passwordValidator;
            _keyGenerator = keyGenerator;
            _privateKeyOptions = privateKeyOptions;
            _dateTime = dateTime;
            _currentUser = currentUser;
        }

        #endregion Constructor

        #region Get

        public async Task<Card> GetAsync(string id)
        {
            var card = await _userManager.FindByIdAsync(id);

            if (card == null)
                throw new EntityNotFoundException(nameof(Card), id);

            return card;
        }

        public async Task<Card> GetByPrivateKeyAsync(string privateKeyXml)
        {
            var cypher = await privateKeyXml.ToAesCypher(_privateKeyOptions.PrivateKeyEncryptionKey,
                _privateKeyOptions.PrivateKeyEncryptionIv);

            var card = await _userManager.Users
                .FirstOrDefaultAsync(c => c.PrivateKeyXml == cypher);

            if (card == null)
                throw new EntityNotFoundException(nameof(Card), privateKeyXml);

            return card;
        }

        #endregion Get

        #region Active

        public async Task<bool> IsActiveAsync(string id)
        {
            var card = await GetAsync(id);

            return card.Active;
        }

        #endregion Active

        #region SignIn

        public async Task<(Result result, string token)> LoginAsync(LoginModel model)
        {
            Card card;

            // Is the card exist?
            try
            {
                card = await GetAsync(model.PrivateKey);
            }
            catch (EntityNotFoundException)
            {
                return (Result.Failure("محاولة تسجيل دخول غير صحيحة."), null);
            }

            // Is card active?
            if (!card.Active)
                return (Result.Failure("هذه البطاقة معطلة."), null);

            // Is card valid?
            if (card.TerminationDate < _dateTime.Now)
                return (Result.Failure("هذه البطاقة منتهية الصلاحية."), null);

            // Is card locked out
            if (await _userManager.IsLockedOutAsync(card))
                return (Result.Failure("لقد حاولت عدة مرات تم تعطيل الحساب لبضع دقائق حاول في وقت لاحق."), null);

            // Is this a correct PIN?
            var isCorrectPin = card.Pin1 == model.Pin1;

            if (isCorrectPin) return (Result.Success(), GenerateJwtToken(card));

            // try to signin with invalid password to increase lockout on failure counter
            // to enabled lockout account if failed to sign a lot of time
            await _signInManager.PasswordSignInAsync(card, "123", false, true);

            return (Result.Failure("محاولة تسجيل دخول غير صحيحة."), null);
        }

        #endregion SignIn

        #region Register

        public async Task<(Result result, string cardId)> RegisterAsync(CreateCardDto model)
        {
            // TODO: hash Pin1 & Pin2 first
            var card = new Card
            {
                Id = Guid.NewGuid().ToString(),
                PublicKeyXml = _keyGenerator.PublicKeyXml,
                PrivateKeyXml = await _keyGenerator.PrivateKeyXml
                    .ToAesCypher(_privateKeyOptions.PrivateKeyEncryptionIv, _privateKeyOptions.PrivateKeyEncryptionKey),
                CitizenId = model.OwnerId,
                Pin1 = model.Pin1,
                Pin2 = model.Pin2,
                Active = true,
                IssueDate = DateTime.UtcNow.AddHours(-2),
                TerminationDate = DateTime.UtcNow.AddYears(4),
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                CardIssuer = _currentUser.UserId,
                UserName = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(card, model.Puk);

            return (result.ToResult(), card.Id);
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
                return result.ToResult();

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
                return result.ToResult();

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
                return validPass.ToResult();

            // change

            var result = await _userManager.ChangePasswordAsync(card, model.CurrentPuk, model.NewPuk);

            return result.ToResult();
        }

        #endregion Change Passwords

        #region Change Active State

        public async Task<Result> InactivateAsync(string id)
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

        public async Task<Result> ActivateAsync(string id)
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

        public Task<bool> IsActiveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion Helper Methods
    }
}
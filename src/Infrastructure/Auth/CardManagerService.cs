using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EGID.Application.Cards.Commands;
using EGID.Application.CivilAffairs.Queries;
using EGID.Application.Common.Exceptions;
using EGID.Application.Common.Interfaces;
using EGID.Common.Models.Result;
using EGID.Domain.Entities;
using EGID.Infrastructure.Common;
using EGID.Infrastructure.Data;
using EGID.Infrastructure.Security.Hash;
using EGID.Infrastructure.Security.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EGID.Infrastructure.Auth
{
    public class CardManagerService : ICardManagerService
    {
        private readonly EgidDbContext _context;
        private readonly UserManager<Card> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Card> _signInManager;
        private readonly IPasswordValidator<Card> _passwordValidator;

        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUser;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IHashService _hashService;

        #region Constructor

        public CardManagerService(
            EgidDbContext context,
            UserManager<Card> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<Card> signInManager,
            IPasswordValidator<Card> passwordValidator,
            IDateTime dateTime,
            ICurrentUserService currentUser, IHashService hashService,
            IJwtTokenService jwtTokenService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordValidator = passwordValidator;
            _dateTime = dateTime;
            _currentUser = currentUser;
            _hashService = hashService;
            _jwtTokenService = jwtTokenService;
            _roleManager = roleManager;
        }

        #endregion Constructor

        #region Get

        public Task<bool> AnyAsync => _userManager.Users.AnyAsync();

        public async Task<Card> GetAsync(string id)
        {
            var card = await _userManager.FindByIdAsync(id);

            if (card == null)
                throw new EntityNotFoundException(nameof(Card), id);

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

        public async Task<(Result result, string token)> LoginAsync(string cardId, string pin1)
        {
            Card card;

            // Is the card exist?
            try
            {
                card = await GetAsync(cardId);
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
            var isCorrectPin = card.Pin1Hash == _hashService.Create(pin1, card.Pin1Salt);

            if (isCorrectPin)
            {
                var token = _jwtTokenService.Generate(new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, card.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                });

                return (Result.Success(), token);
            }

            // try to signin with invalid password to increase lockout on failure counter
            // to enabled lockout account if failed to sign a lot of time
            await _signInManager.PasswordSignInAsync(card, "123", false, true);

            return (Result.Failure("محاولة تسجيل دخول غير صحيحة."), null);
        }

        #endregion SignIn

        #region Register

        public async Task<(Result result, string cardId)> RegisterAsync(CreateCardCommand model)
        {
            var salt1 = Guid.NewGuid().ToString();
            var salt2 = Guid.NewGuid().ToString();

            var card = new Card
            {
                Id = Guid.NewGuid().ToString(),
                CitizenId = model.OwnerId,
                Pin1Hash = _hashService.Create(model.Pin1, salt1),
                Pin1Salt = salt1,
                Pin2Hash = _hashService.Create(model.Pin2, salt2),
                Pin2Salt = salt2,
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

        public async Task<(Result result, string cardId)> RegisterAdminAsync(
            string ownerId,
            string puk,
            string email,
            string phone)
        {
            var salt1 = Guid.NewGuid().ToString();
            var salt2 = Guid.NewGuid().ToString();

            var card = new Card
            {
                Id = Guid.NewGuid().ToString(),
                CardIssuer = ownerId,
                UserName = ownerId,
                CitizenId = ownerId,
                Pin1Hash = _hashService.Create(puk, salt1),
                Pin1Salt = salt1,
                Pin2Hash = _hashService.Create(puk, salt2),
                Pin2Salt = salt2,
                Active = true,
                IssueDate = DateTime.UtcNow.AddHours(-2),
                TerminationDate = DateTime.UtcNow.AddYears(400),
                Email = email,
                PhoneNumber = phone
            };

            var result = await _userManager.CreateAsync(card, puk);

            return (result.ToResult(), card.Id);
        }

        #endregion Register

        #region Change Passwords

        public async Task<Result> ChangePin1Async(string cardId, string puk, string newPin1)
        {
            var card = await GetAsync(cardId);

            // verify Puk
            var signInResult = await _signInManager
                .CheckPasswordSignInAsync(card, puk, true);

            if (!signInResult.Succeeded)
                return Result.Failure(!signInResult.IsLockedOut
                    ? "رمز Puk غير صحيح."
                    : "لقد حاولت عدة مرات تم تعطيل الحساب لبضع دقائق حاول في وقت لاحق."
                );

            // validate new pin1

            var result = await _passwordValidator.ValidateAsync(_userManager, card, newPin1);

            if (!result.Succeeded)
                return result.ToResult();

            // change

            card.Pin1Hash = _hashService.Create(newPin1, Guid.NewGuid().ToString());

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

        public async Task<Result> ChangePin2Async(string cardId, string puk, string newPin2)
        {
            var card = await GetAsync(cardId);

            // verify Puk
            var signInResult = await _signInManager
                .CheckPasswordSignInAsync(card, puk, true);

            if (!signInResult.Succeeded)
                return Result.Failure(!signInResult.IsLockedOut
                    ? "رمز Puk غير صحيح."
                    : "لقد حاولت عدة مرات تم تعطيل الحساب لبضع دقائق حاول في وقت لاحق."
                );

            // validate new pin2

            var result = await _passwordValidator.ValidateAsync(_userManager, card, newPin2);

            if (!result.Succeeded)
                return result.ToResult();

            // change

            card.Pin2Hash = _hashService.Create(newPin2, Guid.NewGuid().ToString());

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

        public async Task<Result> ChangePukAsync(string cardId, string currentPuk, string newPuk)
        {
            var card = await GetAsync(cardId);

            // check provided current PUK

            var checkPasswordResult = await _signInManager
                .CheckPasswordSignInAsync(card, currentPuk, true);

            if (!checkPasswordResult.Succeeded)
                return Result.Failure(!checkPasswordResult.IsLockedOut
                    ? "رمز Puk غير صحيح."
                    : "لقد حاولت عدة مرات تم تعطيل الحساب لبضع دقائق حاول في وقت لاحق."
                );

            // validate new PUK
            var validPass = await _passwordValidator
                .ValidateAsync(_userManager, card, newPuk);

            if (!validPass.Succeeded)
                return validPass.ToResult();

            // change
            var result = await _userManager.ChangePasswordAsync(card, currentPuk, newPuk);

            return result.ToResult();
        }

        #endregion Change Passwords

        #region Change Active State

        public async Task<Result> InactivateAsync(string id)
        {
            var card = await GetAsync(id);

            card.Inactivate();

            _context.Cards.Update(card);

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

            _context.Cards.Update(card);

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

        public async Task<Result> AddToRoleAsync(string cardId, string role)
        {
            var card = await GetAsync(cardId);

            var result = await _userManager.AddToRoleAsync(card, role);

            return result.ToResult();
        }

        public async Task<Result> DeleteAsync(string cardId)
        {
            var card = await GetAsync(cardId);

            var result = await _userManager.DeleteAsync(card);

            return result.ToResult();
        }

        #endregion Change Active State

        #region Roles

        public async Task<IList<EmployeesVm>> InRole(string roleName)
        {
            var role = await _context.Roles
                .SingleOrDefaultAsync(r => r.NormalizedName == roleName.ToUpper());

            if (role is null) return new List<EmployeesVm>();

            var query = from userRole in _context.UserRoles
                        join card in _context.Users on userRole.UserId equals card.Id
                        where userRole.RoleId.Equals(role.Id)
                        join citizen in _context.CitizenDetails on card.CitizenId equals citizen.Id
                        select new EmployeesVm
                        {
                            Id = citizen.Id,
                            CardId = card.Id,
                            FullName = citizen.FullName,
                            Address = citizen.Address,
                            Gender = citizen.Gender,
                            Religion = citizen.Religion,
                            SocialStatus = citizen.SocialStatus,
                            DateOfBirth = citizen.DateOfBirth,
                            PhotoUrl = citizen.PhotoUrl
                        };

            return await query.ToListAsync();
        }

        public Task<bool> AnyRoleAsync => _roleManager.Roles.AnyAsync();

        public async Task<Result> CreateRoleAsync(string roleName)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            return result.ToResult();
        }

        public async Task<Result> RemoveFromRoleAsync(string cardId, string roleName)
        {
            var card = await _userManager.FindByIdAsync(cardId);

            if (card is null) return Result.Failure();

            var result = await _userManager.RemoveFromRoleAsync(card, roleName);

            return result.ToResult();
        }

        public async Task<bool> IsInRoleAsync(string cardId, string roleName)
        {
            var card = await _userManager.FindByIdAsync(cardId);

            return await _userManager.IsInRoleAsync(card, roleName);
        }

        #endregion
    }
}
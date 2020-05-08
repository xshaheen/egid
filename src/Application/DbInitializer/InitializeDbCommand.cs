using System;
using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common;
using EGID.Application.Common.Interfaces;
using EGID.Domain.Entities;
using EGID.Domain.Enums;
using EGID.Domain.ValueObjects;
using MediatR;

namespace EGID.Application.DbInitializer
{
    public class InitializeDbCommand : IRequest
    {
        public class InitializeDbHandler : IRequestHandler<InitializeDbCommand>
        {
            private readonly ICardManagerService _cardManager;
            private readonly IKeysGeneratorService _citizenKeys;
            private readonly IEgidDbContext _context;
            private readonly ISymmetricCryptographyService _cryptographyService;

            public InitializeDbHandler(
                ICardManagerService cardManager,
                IKeysGeneratorService citizenKeys,
                IEgidDbContext context,
                ISymmetricCryptographyService cryptographyService)
            {
                _citizenKeys = citizenKeys;
                _context = context;
                _cryptographyService = cryptographyService;
                _cardManager = cardManager;
            }

            public async Task<Unit> Handle(InitializeDbCommand request, CancellationToken _)
            {
                await InitializeRoles();
                await InitializeAdmin();

                return Unit.Value;
            }

            private async Task InitializeRoles()
            {
                if (await _cardManager.AnyRoleAsync) return;

                foreach (var role in Roles.GetRoles())
                    await _cardManager.CreateRoleAsync(role);
            }

            private async Task InitializeAdmin()
            {
                if (await _cardManager.AnyAsync) return;

                var id = Guid.NewGuid().ToString();

                var adminCitizen = new CitizenDetail
                {
                    Id = id,
                    FullName = new FullName("Mahmoud", "Hamdy", "Shaheen", "Shaheen"),
                    Address = new Address("Street", "City", "State", "Country", "12345"),
                    PhotoUrl = "admin.png",
                    Create = DateTime.Now,
                    PrivateKey = await _cryptographyService.EncryptAsync(_citizenKeys.PrivateKeyXml),
                    PublicKey = _citizenKeys.PublicKeyXml,
                    Religion = Religion.Muslim,
                    Gender = Gender.Male,
                    SocialStatus = SocialStatus.Single,
                };

                await _context.CitizenDetails.AddAsync(adminCitizen);

                await _context.SaveChangesAsync();

                var (result, cardId) = await _cardManager.RegisterAdminAsync(
                    adminCitizen.Id,
                    "#m1234567",
                    "admin@admin.com",
                    "010000000000000"
                );

                if (!result.Succeeded) throw new Exception("failed to initialize admin");

                result = await _cardManager.AddToRoleAsync(cardId, Roles.Admin);

                if (!result.Succeeded) throw new Exception("failed to add admin to the admin role");
            }
        }
    }
}
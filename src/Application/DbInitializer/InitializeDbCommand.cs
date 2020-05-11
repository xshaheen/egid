using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Cards.Commands;
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

                while (_context.CitizenDetails.Count() < 20) await SeedData();

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
                    FullName = new FullName("محمود", "حمدي", "شعبان", "شاهين"),
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

            private async Task SeedData()
            {
                var id1 = Guid.NewGuid().ToString();
                var health1 = Guid.NewGuid().ToString();

                var citizen1 = new CitizenDetail
                {
                    Id = id1,
                    FullName = new FullName("محمد", "محمود", "حسين", "سلامة"),
                    Address = new Address(),
                    Gender = Gender.Male,
                    Religion = Religion.Muslim,
                    SocialStatus = SocialStatus.Married,
                    DateOfBirth = DateTime.Now,
                    PhotoUrl = "test.jfif",
                    HealthInfo = new HealthInfo()
                    {
                        Id = health1,
                        BloodType = BloodType.None,
                        Phone1 = "010123123456",
                        Phone2 = "010133123456",
                        Phone3 = "010113123456",
                    },
                    Create = DateTime.Now,
                    CreateBy = "lorem ipsum",
                };

                await _context.CitizenDetails.AddAsync(citizen1);

                await _cardManager.RegisterAsync(new CreateCardCommand
                {
                    Puk = "#123456",
                    Pin1 = "#123456",
                    Pin2 = "#123456",
                    OwnerId = id1
                });

                var record1 = new HealthRecord
                {
                    Id = Guid.NewGuid().ToString(),
                    HealthInfoId = health1,
                    Diagnosis = "كسر في عظام الساق نتيجة حادث موتوسيكل",
                    Medications = "الادوية مرفقة مرفعة كصورة من الروشته",
                    Attachments = new List<HealthRecordAttachment>
                    {
                        new HealthRecordAttachment
                        {
                            Id = "radiology.jpg"
                        },
                        new HealthRecordAttachment
                        {
                            Id = "roshta.jpg"
                        }
                    }
                };

                var record2 = new HealthRecord
                {
                    Id = Guid.NewGuid().ToString(),
                    HealthInfoId = health1,
                    Diagnosis = "كسر في عظام الساق نتيجة حادث موتوسيكل",
                    Medications = "الادوية مرفقة مرفعة كصورة من الروشته",
                    Attachments = new List<HealthRecordAttachment>
                    {
                        new HealthRecordAttachment
                        {
                            Id = "radiology.jpg"
                        },
                        new HealthRecordAttachment
                        {
                            Id = "roshta.jpg"
                        }
                    }
                };

                var record3 = new HealthRecord
                {
                    Id = Guid.NewGuid().ToString(),
                    HealthInfoId = health1,
                    Diagnosis = "كسر في عظام الساق نتيجة حادث موتوسيكل",
                    Medications = "الادوية مرفقة مرفعة كصورة من الروشته",
                    Attachments = new List<HealthRecordAttachment>
                    {
                        new HealthRecordAttachment
                        {
                            Id = "radiology.jpg"
                        },
                        new HealthRecordAttachment
                        {
                            Id = "roshta.jpg"
                        }
                    }
                };

                var record4 = new HealthRecord
                {
                    Id = Guid.NewGuid().ToString(),
                    HealthInfoId = health1,
                    Diagnosis = "كسر في عظام الساق نتيجة حادث موتوسيكل",
                    Medications = "الادوية مرفقة مرفعة كصورة من الروشته",
                    Attachments = new List<HealthRecordAttachment>
                    {
                        new HealthRecordAttachment
                        {
                            Id = "radiology.jpg"
                        },
                        new HealthRecordAttachment
                        {
                            Id = "roshta.jpg"
                        }
                    }
                };

                var record5 = new HealthRecord
                {
                    Id = Guid.NewGuid().ToString(),
                    HealthInfoId = health1,
                    Diagnosis = "كسر في عظام الساق نتيجة حادث موتوسيكل",
                    Medications = "الادوية مرفقة مرفعة كصورة من الروشته",
                    Attachments = new List<HealthRecordAttachment>
                    {
                        new HealthRecordAttachment
                        {
                            Id = "radiology.jpg"
                        },
                        new HealthRecordAttachment
                        {
                            Id = "roshta.jpg"
                        }
                    }
                };

                await _context.HealthRecords.AddAsync(record1);
                await _context.HealthRecords.AddAsync(record2);
                await _context.HealthRecords.AddAsync(record3);
                await _context.HealthRecords.AddAsync(record4);
                await _context.HealthRecords.AddAsync(record5);
            }
        }
    }
}
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
using Microsoft.Extensions.Configuration;

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
            private readonly IConfiguration _config;

            public InitializeDbHandler(
                ICardManagerService cardManager,
                IKeysGeneratorService citizenKeys,
                IEgidDbContext context,
                ISymmetricCryptographyService cryptographyService,
                IConfiguration config)
            {
                _citizenKeys = citizenKeys;
                _context = context;
                _cryptographyService = cryptographyService;
                _config = config;
                _cardManager = cardManager;
            }

            public async Task<Unit> Handle(InitializeDbCommand request, CancellationToken _)
            {
                await InitializeRoles();
                await InitializeAdmin();

                if (_context.CitizenDetails.Count() < 20) await SeedData();
                // seed 1 have no health info so no conflict with attachments id
                while (_context.CitizenDetails.Count() < 20) await SeedData1();

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

                var adminCitizen = new CitizenDetail
                {
                    Id = _config["Admin:citizenId"],
                    FullName = new FullName("محمود", "حمدي", "شعبان", "شاهين"),
                    Address = new Address("Street", "City", "State", "Country", "12345"),
                    PhotoUrl = "admin.png",
                    Create = DateTime.Now,
                    PrivateKey = await _cryptographyService.EncryptAsync(_citizenKeys.PrivateKeyXml),
                    PublicKey = _citizenKeys.PublicKeyXml,
                    Religion = Religion.Muslim,
                    Gender = Gender.Male,
                    SocialStatus = SocialStatus.Single,
                    HealthInfo = new HealthInfo
                    {
                        Id = Guid.NewGuid().ToString(),
                        BloodType = BloodType.A,
                        Phone1 = "+20101234567"
                    }
                };

                await _context.CitizenDetails.AddAsync(adminCitizen);

                await _context.SaveChangesAsync();

                var (result, cardId) = await _cardManager.RegisterAdminAsync(
                    _config["Admin:cardId"],
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
                    PrivateKey = await _cryptographyService.EncryptAsync(_citizenKeys.PrivateKeyXml),
                    PublicKey = _citizenKeys.PublicKeyXml,
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

                await _context.SaveChangesAsync();

                await _cardManager.RegisterAsync(new CreateCardCommand
                {
                    Puk = "#m123456",
                    Pin1 = "#m123456",
                    Pin2 = "#m123456",
                    OwnerId = id1
                });


                citizen1.HealthInfo.HealthRecords.Add(
                    new HealthRecord
                    {
                        Id = Guid.NewGuid().ToString(),
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
                    }
                );

                citizen1.HealthInfo.HealthRecords.Add(
                    new HealthRecord
                    {
                        Id = Guid.NewGuid().ToString(),
                        HealthInfoId = health1,
                        Diagnosis = "كسر في عظام الساق نتيجة حادث موتوسيكل",
                        Medications = "الادوية مرفقة مرفعة كصورة من الروشته",
                        Attachments = new List<HealthRecordAttachment>
                        {
                            new HealthRecordAttachment
                            {
                                Id = "radiology - Copy (2).jpg"
                            },
                            new HealthRecordAttachment
                            {
                                Id = "roshta - Copy (2).jpg"
                            }
                        }
                    }
                );

                citizen1.HealthInfo.HealthRecords.Add(
                    new HealthRecord
                    {
                        Id = Guid.NewGuid().ToString(),
                        HealthInfoId = health1,
                        Diagnosis = "كسر في عظام الساق نتيجة حادث موتوسيكل",
                        Medications = "الادوية مرفقة مرفعة كصورة من الروشته",
                        Attachments = new List<HealthRecordAttachment>
                        {
                            new HealthRecordAttachment
                            {
                                Id = "radiology - Copy (3).jpg"
                            },
                            new HealthRecordAttachment
                            {
                                Id = "roshta - Copy (3).jpg"
                            }
                        }
                    }
                );

                citizen1.HealthInfo.HealthRecords.Add(
                    new HealthRecord
                    {
                        Id = Guid.NewGuid().ToString(),
                        HealthInfoId = health1,
                        Diagnosis = "كسر في عظام الساق نتيجة حادث موتوسيكل",
                        Medications = "الادوية مرفقة مرفعة كصورة من الروشته",
                        Attachments = new List<HealthRecordAttachment>
                        {
                            new HealthRecordAttachment
                            {
                                Id = "radiology - Copy (4).jpg"
                            },
                            new HealthRecordAttachment
                            {
                                Id = "roshta - Copy (4).jpg"
                            }
                        }
                    }
                );

                citizen1.HealthInfo.HealthRecords.Add(
                    new HealthRecord
                    {
                        Id = Guid.NewGuid().ToString(),
                        HealthInfoId = health1,
                        Diagnosis = "كسر في عظام الساق نتيجة حادث موتوسيكل",
                        Medications = "الادوية مرفقة مرفعة كصورة من الروشته",
                        Attachments = new List<HealthRecordAttachment>
                        {
                            new HealthRecordAttachment
                            {
                                Id = "radiology - Copy.jpg"
                            },
                            new HealthRecordAttachment
                            {
                                Id = "roshta - Copy.jpg"
                            }
                        }
                    }
                );


                await _context.SaveChangesAsync();
            }

            private async Task SeedData1()
            {
                var id1 = Guid.NewGuid().ToString();
                var health1 = Guid.NewGuid().ToString();

                var citizen1 = new CitizenDetail
                {
                    Id = id1,
                    FullName = new FullName("محمود", "محمود", "حمدي", "شاهين"),
                    Address = new Address(),
                    Gender = Gender.Male,
                    Religion = Religion.Muslim,
                    SocialStatus = SocialStatus.Married,
                    DateOfBirth = DateTime.Now,
                    PrivateKey = await _cryptographyService.EncryptAsync(_citizenKeys.PrivateKeyXml),
                    PublicKey = _citizenKeys.PublicKeyXml,
                    PhotoUrl = "test.jfif",
                    HealthInfo = new HealthInfo()
                    {
                        Id = health1,
                        BloodType = BloodType.None,
                        Phone1 = "010123123456",
                        Phone2 = "010133123456",
                        Phone3 = "010113123456",
                    },
                };

                await _context.CitizenDetails.AddAsync(citizen1);

                await _context.SaveChangesAsync();

                await _cardManager.RegisterAsync(new CreateCardCommand
                {
                    Puk = "#m123456",
                    Pin1 = "#m123456",
                    Pin2 = "#m123456",
                    OwnerId = id1
                });
            }
        }
    }
}
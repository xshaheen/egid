using System;
using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common.Interfaces;
using EGID.Application.Common.Models.Files;
using EGID.Domain.Entities;
using EGID.Domain.Enums;
using EGID.Domain.ValueObjects;
using FluentValidation;
using MediatR;

namespace EGID.Application.CitizenDetails.Commands
{
    public class CreateCitizenCommand : IRequest<string>
    {
        public string FatherId { get; set; }
        public string MotherId { get; set; }

        public FullName FullName { get; set; }
        public Address Address { get; set; }

        public Gender Gender { get; set; }
        public Religion Religion { get; set; }
        public SocialStatus SocialStatus { get; set; }
        public DateTime DateOfBirth { get; set; }

        public BinaryFile Photo { get; set; }

        public BloodType BloodType { get; set; }
        public string HealthEmergencyPhone1 { get; set; }
        public string HealthEmergencyPhone2 { get; set; }
        public string HealthEmergencyPhone3 { get; set; }

        #region Validator

        public class CreateCitizenValidator : AbstractValidator<CreateCitizenCommand>
        {
            public CreateCitizenValidator()
            {
                RuleFor(x => x.MotherId).MaximumLength(128);
                RuleFor(x => x.FatherId).MaximumLength(128);

                RuleFor(x => x.FullName).NotNull();
                RuleFor(x => x.FullName.FirstName).NotEmpty().MaximumLength(50);
                RuleFor(x => x.FullName.SecondName).NotEmpty().MaximumLength(50);
                RuleFor(x => x.FullName.ThirdName).NotEmpty().MaximumLength(50);
                RuleFor(x => x.FullName.LastName).NotEmpty().MaximumLength(50);

                RuleFor(x => x.Address).NotNull();

                RuleFor(x => x.Address.Country).NotEmpty();
                RuleFor(x => x.Address.State).NotEmpty();
                RuleFor(x => x.Address.City).NotEmpty();

                RuleFor(x => x.Gender).NotEqual(Gender.None);
                RuleFor(x => x.Religion).NotEqual(Religion.None);
                RuleFor(x => x.SocialStatus).NotEqual(SocialStatus.None);

                RuleFor(x => x.DateOfBirth).NotEqual((DateTime) default);

                RuleFor(x => x.Photo).NotNull();
                RuleFor(x => x.Photo.Bytes).NotNull();
                RuleFor(x => x.Photo.Name).NotNull();
            }
        }

        #endregion

        #region Handler

        public class CreateCitizenCommandHandler : IRequestHandler<CreateCitizenCommand, string>
        {
            private readonly IEgidDbContext _context;
            private readonly IFilesDirectoryService _directories;
            private readonly IKeysGeneratorService _keys;
            private readonly ISymmetricCryptographyService _cryptographyService;

            public CreateCitizenCommandHandler(IEgidDbContext context, IFilesDirectoryService directories,
                ISymmetricCryptographyService cryptographyService, IKeysGeneratorService keys)
            {
                _context = context;
                _directories = directories;
                _cryptographyService = cryptographyService;
                _keys = keys;
            }

            public async Task<string> Handle(CreateCitizenCommand request, CancellationToken cancellationToken)
            {
                await request.Photo.SaveAsync(_directories.CitizenPhotosDirectory);

                var citizen = new CitizenDetail
                {
                    PrivateKey = await _cryptographyService.EncryptAsync(_keys.PrivateKeyXml),
                    PublicKey = _keys.PublicKeyXml,
                    Id = Guid.NewGuid().ToString(),
                    FatherId = request.FatherId,
                    MotherId = request.MotherId,
                    FullName = request.FullName,
                    Address = request.Address,
                    Gender = request.Gender,
                    Religion = request.Religion,
                    SocialStatus = request.SocialStatus,
                    DateOfBirth = request.DateOfBirth,
                    PhotoUrl = request.Photo.Name,
                    HealthInfo = new HealthInfo
                    {
                        Phone1 = request.HealthEmergencyPhone1,
                        Phone2 = request.HealthEmergencyPhone2,
                        Phone3 = request.HealthEmergencyPhone3,
                        BloodType = request.BloodType
                    }
                };

                await _context.CitizenDetails.AddAsync(citizen, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                return citizen.Id;
            }
        }

        #endregion
    }
}
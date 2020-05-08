using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common;
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

        [Required] public FullName FullName { get; set; }
        [Required] public Address Address { get; set; }

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
                RuleFor(x => x.MotherId)
                    .MaximumLength(128).WithMessage("خطأ هذا العنصر غير مطابق النمط الصحيح");

                RuleFor(x => x.FatherId)
                    .MaximumLength(128).WithMessage("خطأ هذا العنصر غير مطابق النمط الصحيح");

                RuleFor(x => x.FullName)
                    .NotNull().WithMessage("يرجي ادخال اسم المواطن");
                RuleFor(x => x.FullName.FirstName)
                    .NotEmpty().WithMessage("يرجي ادخال الاسم الاول للمواطن.")
                    .MaximumLength(50).WithMessage("الاسم لايمكن ان يتجاوز الخمسون حرفا.");
                RuleFor(x => x.FullName.SecondName)
                    .NotEmpty().WithMessage("يرجي ادخال الاسم الثاني للمواطن.")
                    .MaximumLength(50).WithMessage("الاسم لايمكن ان يتجاوز الخمسون حرفا.");
                RuleFor(x => x.FullName.ThirdName)
                    .NotEmpty().WithMessage("يرجي ادخال الثالث للمواطن.")
                    .MaximumLength(50).WithMessage("الاسم لايمكن ان يتجاوز الخمسون حرفا.");
                RuleFor(x => x.FullName.LastName)
                    .NotEmpty().WithMessage("يرجي ادخال الاسم الاخير للمواطن.")
                    .MaximumLength(50).WithMessage("الاسم لايمكن ان يتجاوز الخمسون حرفا.");

                RuleFor(x => x.Address)
                    .NotNull().WithMessage("يرجي اضافة العنوان.");

                RuleFor(x => x.Address.Country)
                    .NotEmpty().WithMessage("يرجي اختيار الدولة المقيم بها المواطن حاليا.");

                RuleFor(x => x.Address.State)
                    .NotEmpty().WithMessage("يرجي اختيار المحافظة التابع لها المواطن.");

                RuleFor(x => x.Address.City)
                    .NotEmpty().WithMessage("يرجي اختيار المدينة التابع لها المواطن.");

                RuleFor(x => x.Gender)
                    .NotEqual(Gender.None).WithMessage("يرجي ادخال النوع.");

                RuleFor(x => x.Religion)
                    .NotEqual(Religion.None).WithMessage("يرجي اختيار الديانة.");

                RuleFor(x => x.SocialStatus)
                    .NotEqual(SocialStatus.None).WithMessage("يرجي اختيار الحالة الاجتماعية.");

                RuleFor(x => x.DateOfBirth)
                    .NotEqual((DateTime) default).WithMessage("اختر تاريخ ميلاد صحيح.");

                RuleFor(x => x.Photo)
                    .NotNull().WithMessage("ارفق صورة المواطن.");

                RuleFor(x => x.Photo.Bytes)
                    .NotNull().WithMessage("الصورة غير صحيحية");

                RuleFor(x => x.Photo.Name)
                    .NotNull().WithMessage("الصورة غير صحيحية");

                RuleFor(x => x.HealthEmergencyPhone1)
                    .MaximumLength(24).WithMessage("رقم الهاتف لايمكن ان يتجاوز 24 رقما")
                    .Matches(Regexes.InternationalPhone)
                    .WithMessage("رقم الهاتف غير صحيح.");

                RuleFor(x => x.HealthEmergencyPhone2)
                    .MaximumLength(24).WithMessage("رقم الهاتف لايمكن ان يتجاوز 24 رقما")
                    .Matches(Regexes.InternationalPhone)
                    .WithMessage("رقم الهاتف غير صحيح.");

                RuleFor(x => x.HealthEmergencyPhone3)
                    .MaximumLength(24).WithMessage("رقم الهاتف لايمكن ان يتجاوز 24 رقما")
                    .Matches(Regexes.InternationalPhone)
                    .WithMessage("رقم الهاتف غير صحيح.");

                RuleFor(x => x.BloodType)
                    .NotEqual(BloodType.None).WithMessage("يرجي اختيار فصيلة الدم.");
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

                return citizen.HealthInfo.Id;
            }
        }

        #endregion
    }
}
using System;
using EGID.Domain.Enums;
using FluentValidation;

namespace EGID.Application.CitizenDetails.Commands.CreateCitizen
{
    public class CreateCitizenValidator : AbstractValidator<CreateCitizenCommand>
    {
        public CreateCitizenValidator()
        {
            RuleFor(x => x.AccountId).MaximumLength(128);
            RuleFor(x => x.MotherId).MaximumLength(128);
            RuleFor(x => x.FatherId).MaximumLength(128);

            RuleFor(x => x.FullName).NotNull();

            RuleFor(x => x.FullName.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.FullName.SecondName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.FullName.ThirdName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.FullName.LastName).NotEmpty().MaximumLength(50);

            RuleFor(x => x.Gender).NotEqual(Gender.None);
            RuleFor(x => x.Religion).NotEqual(Religion.None);
            RuleFor(x => x.SocialStatus).NotEqual(SocialStatus.None);

            RuleFor(x => x.DateOfBirth).NotEqual((DateTime) default);

            RuleFor(x => x.Photo).NotNull();
            RuleFor(x => x.Photo.Bytes).NotNull();
            RuleFor(x => x.Photo.Name).NotNull();
        }
    }
}
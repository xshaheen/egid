using System;
using EGID.Domain.Enums;
using EGID.Domain.ValueObjects;
using FluentValidation;

namespace EGID.Application.CitizenDetails.Commands.UpdateCitizen
{
    public class UpdateCitizenValidator : AbstractValidator<UpdateCitizenCommand>
    {
        public UpdateCitizenValidator()
        {
            RuleFor(x => x.AccountId).MaximumLength(128);
            RuleFor(x => x.MotherId).MaximumLength(128);
            RuleFor(x => x.FatherId).MaximumLength(128);

            RuleFor(x => x.FullName).NotNull();

            RuleFor(x => x.FullName.FirstName)
                .MaximumLength(50)
                .When(x => x.FullName?.FirstName != null);

            RuleFor(x => x.FullName.SecondName)
                .MaximumLength(50)
                .When(x => x.FullName?.SecondName != null);

            RuleFor(x => x.FullName.ThirdName)
                .MaximumLength(50)
                .When(x => x.FullName?.ThirdName != null);

            RuleFor(x => x.FullName.LastName)
                .MaximumLength(50)
                .When(x => x.FullName?.LastName != null);
        }
    }
}

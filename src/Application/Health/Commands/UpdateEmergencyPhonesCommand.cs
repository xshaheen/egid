using System;
using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common;
using EGID.Application.Common.Exceptions;
using EGID.Application.Common.Interfaces;
using EGID.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EGID.Application.Health.Commands
{
    public class UpdateEmergencyPhonesCommand : IRequest
    {
        public string HealthInfoId { get; set; }

        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }

        #region Validator

        public class UpdateEmergencyPhonesValidator : AbstractValidator<UpdateEmergencyPhonesCommand>
        {
            public UpdateEmergencyPhonesValidator()
            {
                RuleFor(x => x.HealthInfoId).NotEmpty().Length(128);

                RuleFor(x => x.Phone1)
                    .MaximumLength(24).WithMessage("رقم الهاتف لايمكن ان يتجاوز 24 رقما")
                    .Matches(Regexes.InternationalPhone)
                    .WithMessage("رقم الهاتف غير صحيح.");

                RuleFor(x => x.Phone2)
                    .MaximumLength(24).WithMessage("رقم الهاتف لايمكن ان يتجاوز 24 رقما")
                    .Matches(Regexes.InternationalPhone)
                    .WithMessage("رقم الهاتف غير صحيح.");

                RuleFor(x => x.Phone3)
                    .MaximumLength(24).WithMessage("رقم الهاتف لايمكن ان يتجاوز 24 رقما")
                    .Matches(Regexes.InternationalPhone)
                    .WithMessage("رقم الهاتف غير صحيح.");
            }
        }

        #endregion

        #region Handler

        public class UpdateHealthInfoHandler : IRequestHandler<UpdateEmergencyPhonesCommand>
        {
            private readonly IEgidDbContext _context;
            private readonly ICurrentUserService _currentUser;

            public UpdateHealthInfoHandler(IEgidDbContext context, ICurrentUserService currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public async Task<Unit> Handle(UpdateEmergencyPhonesCommand request, CancellationToken cancellationToken)
            {
                var healthInfo = await _context.HealthInformation
                    .FirstOrDefaultAsync(c => c.Id == request.HealthInfoId, cancellationToken);

                if (healthInfo is null) throw new EntityNotFoundException(nameof(Card), request.HealthInfoId);

                healthInfo.Phone1 = request.Phone1;
                healthInfo.Phone2 = request.Phone2;
                healthInfo.Phone3 = request.Phone3;

                return Unit.Value;
            }
        }

        #endregion
    }
}
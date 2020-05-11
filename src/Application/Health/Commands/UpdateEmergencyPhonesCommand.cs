using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common.Exceptions;
using EGID.Application.Common.Interfaces;
using EGID.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
// TODO: Validate
namespace EGID.Application.Health.Commands
{
    public class UpdateEmergencyPhonesCommand : IRequest
    {
        public string HealthInfoId { get; set; }

        public string Notes { get; set; }

        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }

        #region Validator

        public class UpdateEmergencyPhonesValidator : AbstractValidator<UpdateEmergencyPhonesCommand>
        {
            public UpdateEmergencyPhonesValidator()
            {
                RuleFor(x => x.HealthInfoId)
                    .NotEmpty().WithMessage("الكود غير مرفق مع الطلب.");

                RuleFor(x => x.Phone1)
                    .MaximumLength(24).WithMessage("رقم الهاتف لايمكن ان يتجاوز 24 رقما");

                RuleFor(x => x.Phone2)
                    .MaximumLength(24).WithMessage("رقم الهاتف لايمكن ان يتجاوز 24 رقما");

                RuleFor(x => x.Phone3)
                    .MaximumLength(24).WithMessage("رقم الهاتف لايمكن ان يتجاوز 24 رقما");
            }
        }

        #endregion

        #region Handler

        public class UpdateHealthInfoHandler : IRequestHandler<UpdateEmergencyPhonesCommand>
        {
            private readonly IEgidDbContext _context;

            public UpdateHealthInfoHandler(IEgidDbContext context) => _context = context;

            public async Task<Unit> Handle(UpdateEmergencyPhonesCommand request, CancellationToken cancellationToken)
            {
                var healthInfo = await _context.HealthInformation
                    .FirstOrDefaultAsync(c => c.Id == request.HealthInfoId, cancellationToken);

                if (healthInfo is null) throw new EntityNotFoundException(nameof(Card), request.HealthInfoId);

                healthInfo.Phone1 = request.Phone1;
                healthInfo.Phone2 = request.Phone2;
                healthInfo.Phone3 = request.Phone3;
                healthInfo.Notes = request.Notes;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }

        #endregion
    }
}
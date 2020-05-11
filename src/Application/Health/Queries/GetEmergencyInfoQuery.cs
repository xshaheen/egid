using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common.Exceptions;
using EGID.Application.Common.Interfaces;
using EGID.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EGID.Application.Health.Queries
{
    public class GetEmergencyInfoQuery : IRequest<EmergencyInfo>
    {
        public string HealthInfoId { get; set; }

        #region Validator

        public class GetEmergencyInfoValidator : AbstractValidator<GetHealthInfoQuery>
        {
            public GetEmergencyInfoValidator()
            {
                RuleFor(e => e.HealthInfoId).NotEmpty();
            }
        }

        #endregion

        #region Handler

        public class GetEmergencyInfoQueryHandler : IRequestHandler<GetEmergencyInfoQuery, EmergencyInfo>
        {
            private readonly IEgidDbContext _context;

            public GetEmergencyInfoQueryHandler(IEgidDbContext context) => _context = context;

            public async Task<EmergencyInfo> Handle(GetEmergencyInfoQuery request, CancellationToken cancellationToken)
            {
                var healthInfo = await _context.HealthInformation
                    .FirstOrDefaultAsync(c => c.Id == request.HealthInfoId, cancellationToken);

                if (healthInfo == null) throw new  EntityNotFoundException(nameof(HealthInfo), request.HealthInfoId);

                return new EmergencyInfo
                {
                    BloodType = healthInfo.BloodType,
                    Notes = healthInfo.Notes,
                    Phone1 = healthInfo.Phone1,
                    Phone2 = healthInfo.Phone2,
                    Phone3 = healthInfo.Phone3
                };
            }
        }

        #endregion

    }
}

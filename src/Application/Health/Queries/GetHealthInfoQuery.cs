using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EGID.Application.Health.Queries
{
    public class GetHealthInfoQuery : IRequest<HealthInfoVm>
    {
        public string HealthInfoId { get; set; }

        #region Validator

        public class GetHealthInfoQueryValidator : AbstractValidator<GetHealthInfoQuery>
        {
            public GetHealthInfoQueryValidator()
            {
                RuleFor(e => e.HealthInfoId).NotEmpty();
            }
        }

        #endregion

        #region Handler

        public class GetHealthInfoQueryHandler : IRequestHandler<GetHealthInfoQuery, HealthInfoVm>
        {
            private readonly IEgidDbContext _context;
            private readonly IFilesDirectoryService _directoryService;

            public GetHealthInfoQueryHandler(IEgidDbContext context, IFilesDirectoryService directoryService)
            {
                _context = context;
                _directoryService = directoryService;
            }

            public async Task<HealthInfoVm> Handle(GetHealthInfoQuery request, CancellationToken cancellationToken)
            {
                var healthInfo = await _context.HealthInformation
                    .Include(h => h.HealthRecords)
                    .Include(h => h.Citizen)
                    .Select(h => new HealthInfoVm
                    {
                        Id = h.Id,
                        CitizenName = h.Citizen.FullName,
                        CitizenPhoto = Path.Combine(_directoryService.CitizenPhotosRelativePath, h.Citizen.PhotoUrl),
                        BloodType = h.BloodType,
                        Phone1 = h.Phone1,
                        Phone2 = h.Phone2,
                        Phone3 = h.Phone3,
                        Notes = h.Notes,
                        HealthRecords = h.HealthRecords.Select(healthRecord => new HealthRecordVm()
                        {
                            Medications = healthRecord.Medications,
                            Diagnosis = healthRecord.Diagnosis,
                            Create = healthRecord.Create,
                            CreateBy = healthRecord.CreateBy,
                            Attachments = healthRecord.Attachments
                                .Select(a =>  Path.Combine(_directoryService.HealthInfoRelativePath, a.HealthRecordId))
                                .ToList()
                        }).ToList()
                    })
                    .FirstOrDefaultAsync(c => c.Id == request.HealthInfoId, cancellationToken);

                return healthInfo;
            }
        }

        #endregion
    }
}
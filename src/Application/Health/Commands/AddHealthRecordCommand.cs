using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common.Exceptions;
using EGID.Application.Common.Interfaces;
using EGID.Application.Common.Models.Files;
using EGID.Domain.Entities;
using FluentValidation;
using MediatR;

namespace EGID.Application.Health.Commands
{
    public class AddHealthRecordCommand : IRequest
    {
        public string HealthInfoId { get; set; }

        public string Medications { get; set; }

        public string Diagnosis { get; set; }

        public virtual ICollection<BinaryFile> Attachments { get; set; }

        #region Validator

        public class AddHealthRecordValidator : AbstractValidator<AddHealthRecordCommand>
        {
            public AddHealthRecordValidator()
            {
                RuleFor(x => x.HealthInfoId)
                    .NotEmpty().Length(128);

                RuleFor(x => x.Diagnosis)
                    .NotEmpty().WithMessage("يرجي ملأ خانة التشخيص.")
                    .MaximumLength(4096).WithMessage("تجاوزت الحد الاقصي عدد للحروف 4096 حرف");

                RuleFor(x => x.Medications)
                    .NotEmpty().WithMessage("يرجي ملأ خانة العلاج.")
                    .MaximumLength(4096).WithMessage("تجاوزت الحد الاقصي عدد للحروف 4096 حرف");
            }
        }

        #endregion

        #region Handler

        public class AddHealthRecordCommandHandler : IRequestHandler<AddHealthRecordCommand>
        {
            private readonly IEgidDbContext _context;
            private readonly IFilesDirectoryService _directoryService;

            public AddHealthRecordCommandHandler(IEgidDbContext context, IFilesDirectoryService directoryService)
            {
                _context = context;
                _directoryService = directoryService;
            }

            public async Task<Unit> Handle(AddHealthRecordCommand request, CancellationToken cancellationToken)
            {
                var healthInfo = await _context.HealthInformation.FindAsync(request.HealthInfoId);

                if (healthInfo is null) throw new EntityNotFoundException(nameof(HealthInfo), request.HealthInfoId);

                healthInfo.HealthRecords.Add(new HealthRecord
                {
                    Id = Guid.NewGuid().ToString(),
                    Medications = request.Medications,
                    Diagnosis = request.Diagnosis,
                    Attachments = (await request.Attachments.SaveAsync(_directoryService.HealthInfoDirectory))
                        .Select(b => new HealthRecordAttachment {Id = b.Name})
                        .ToList()
                });

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }

        #endregion
    }
}
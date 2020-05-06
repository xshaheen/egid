using System.IO;
using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common.Exceptions;
using EGID.Application.Common.Interfaces;
using EGID.Domain.ValueObjects;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EGID.Application.Cards.Commands
{
    public class VerifySignatureCommand : IRequest<(bool valid, FullName name, string Photo)>
    {
        public string Base64Sha512DataHash { get; set; }

        public string Signature { get; set; }

        #region Validator

        public class VerifySignatureValidator : AbstractValidator<VerifySignatureCommand>
        {
            public VerifySignatureValidator()
            {
                RuleFor(s => s.Base64Sha512DataHash)
                    .NotEmpty().WithMessage("من فضلك اضف بصمة الملفات من نوع base64 sha512 للتوقيع.")
                    .MaximumLength(512).WithMessage("خطأ: هذه البيانات غير مطابقة للشروط.");

                RuleFor(s => s.Base64Sha512DataHash)
                    .NotEmpty().WithMessage("من فضلك اضف التوقيع الذي تريد التحقق منه.");
            }
        }

        #endregion

        #region Handler

        public class
            VerifySignatureCommandHandler : IRequestHandler<VerifySignatureCommand, (bool valid, FullName name, string
                Photo)>
        {
            private readonly IDigitalSignatureService _digitalSignatureService;
            private readonly IEgidDbContext _context;
            private readonly IFilesDirectoryService _directory;

            public VerifySignatureCommandHandler(IDigitalSignatureService digitalSignatureService,
                IEgidDbContext context, IFilesDirectoryService directory)
            {
                _digitalSignatureService = digitalSignatureService;
                _context = context;
                _directory = directory;
            }

            public async Task<(bool valid, FullName name, string Photo)> Handle(VerifySignatureCommand request,
                CancellationToken cancellationToken)
            {
                var citizenId = request.Signature[..128];
                var dataSignature = request.Signature[128..];

                var citizen = await _context.CitizenDetails.FirstOrDefaultAsync(c => c.Id == citizenId,
                    cancellationToken);

                if (citizen == null)
                    throw new BadRequestException(new[]
                        {"هذا التوقيع غير صحيح: هذا المواطن غير موجود او هذا التوقيع غير سليم."});

                var valid =
                    _digitalSignatureService.VerifySignature(request.Base64Sha512DataHash, dataSignature,
                        citizen.PublicKey);

                return (valid, citizen.FullName, Path.Combine(_directory.CitizenPhotosDirectory, citizen.PhotoUrl));
            }
        }

        #endregion
    }
}
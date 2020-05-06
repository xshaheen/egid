using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common.Exceptions;
using EGID.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

namespace EGID.Application.Cards.Commands
{
    public class SignHashCommand : IRequest<string>
    {
        public string Base64Sha512DataHash { get; set; }

        #region Validator

        public class SignHashValidator : AbstractValidator<SignHashCommand>
        {
            public SignHashValidator()
            {
                RuleFor(s => s.Base64Sha512DataHash)
                    .NotEmpty().WithMessage("من فضلك اضف بصمة الملفات من نوع base64 sha512 للتوقيع.")
                    .MaximumLength(512).WithMessage("خطأ: هذه البيانات غير مطابقة للشروط.");
            }
        }

        #endregion

        #region Handler

        public class SignHashCommandHandler : IRequestHandler<SignHashCommand, string>
        {
            private readonly IDigitalSignatureService _digitalSignatureService;
            private readonly IEgidDbContext _context;
            private readonly ICurrentUserService _currentUser;

            public SignHashCommandHandler(IDigitalSignatureService digitalSignatureService, IEgidDbContext context,
                ICurrentUserService currentUser)
            {
                _digitalSignatureService = digitalSignatureService;
                _context = context;
                _currentUser = currentUser;
            }

            public async Task<string> Handle(SignHashCommand request, CancellationToken cancellationToken)
            {
                var card = await _context.Cards.FindAsync(_currentUser.UserId);

                if (card is null) throw new EntityNotFoundException("Card", _currentUser.UserId);

                var citizen = await _context.CitizenDetails.FindAsync(card.CitizenId);

                if (citizen is null) throw new EntityNotFoundException("Citizen", card.CitizenId);

                var signature = _digitalSignatureService.SignHash(request.Base64Sha512DataHash, citizen.PrivateKey);

                return _currentUser.UserId + signature;
            }
        }

        #endregion
    }
}
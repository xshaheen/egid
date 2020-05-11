using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common.Exceptions;
using EGID.Application.Common.Interfaces;
using EGID.Application.Common.Models.Files;
using FluentValidation;
using MediatR;

namespace EGID.Application.Cards.Commands
{
    public class SignHashCommand : IRequest<BinaryFile>
    {
        [Required] public string Pin2 { get; set; }

        [Required] public string FileName { get; set; }

        [Required] public string Base64Sha512DataHash { get; set; }

        #region Validator

        public class SignHashValidator : AbstractValidator<SignHashCommand>
        {
            public SignHashValidator()
            {
                RuleFor(s => s.Pin2)
                    .NotEmpty().WithMessage("من فضلك لتوقيع الملفات ادخل رمز PIN2 الخاص بك.");

                RuleFor(s => s.Base64Sha512DataHash)
                    .NotEmpty().WithMessage("من فضلك اضف بصمة الملفات من نوع base64 sha512 للتوقيع.")
                    .MaximumLength(512).WithMessage("خطأ: هذه البيانات غير مطابقة للشروط.");
            }
        }

        #endregion

        #region Handler

        public class SignHashCommandHandler : IRequestHandler<SignHashCommand, BinaryFile>
        {
            private readonly IDigitalSignatureService _digitalSignatureService;
            private readonly IEgidDbContext _context;
            private readonly ICurrentUserService _currentUser;
            private readonly ICardManagerService _cardManager;
            private readonly ISymmetricCryptographyService _cryptoService;

            public SignHashCommandHandler(IDigitalSignatureService digitalSignatureService, IEgidDbContext context,
                ICurrentUserService currentUser, ICardManagerService cardManager, ISymmetricCryptographyService cryptoService)
            {
                _digitalSignatureService = digitalSignatureService;
                _context = context;
                _currentUser = currentUser;
                _cardManager = cardManager;
                _cryptoService = cryptoService;
            }

            public async Task<BinaryFile> Handle(SignHashCommand request, CancellationToken cancellationToken)
            {
                var card = await _context.Cards.FindAsync(_currentUser.CardId);

                if (card is null) throw new EntityNotFoundException("Card", _currentUser.CardId);

                if (!_cardManager.VerifyPin2(card, request.Pin2))
                    throw new BadRequestException(new[] {"خطأ رمز PIN2 غير صحيح."});

                var citizen = await _context.CitizenDetails.FindAsync(_currentUser.CitizenId);

                if (citizen is null) throw new EntityNotFoundException("Citizen", card.CitizenId);

                var signature = _digitalSignatureService.SignHash(request.Base64Sha512DataHash, await _cryptoService.DecryptAsync(citizen.PrivateKey));

                var s = citizen.Id + signature;

                var memory = new MemoryStream();
                var writer = new StreamWriter(memory);

                await writer.WriteAsync(s);
                await writer.FlushAsync();

                var name = Path.GetFileNameWithoutExtension(request.FileName) + ".sign";
                var bytes = memory.ToArray();

                return new BinaryFile(name, bytes, bytes.Length, "text/plain");
            }
        }

        #endregion
    }
}
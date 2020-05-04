using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EGID.Common.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
                var citizen = await _context.CitizenDetails.FirstOrDefaultAsync(c => c.CardId == _currentUser.UserId,
                    cancellationToken: cancellationToken);

                if (citizen == null) throw new EntityNotFoundException("Card", _currentUser.UserId);

                var signature = _digitalSignatureService.SignHash(request.Base64Sha512DataHash, citizen.PrivateKey);

                return _currentUser.UserId + signature;
            }
        }

        #endregion
    }
}
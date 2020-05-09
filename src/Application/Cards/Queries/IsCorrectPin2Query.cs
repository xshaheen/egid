using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Cards.Commands;
using EGID.Application.Common.Exceptions;
using EGID.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

namespace EGID.Application.Cards.Queries
{
    public class IsCorrectPin2Query : IRequest<bool>
    {
        [Required] public string Pin2 { get; set; }

        #region Validator

        public class SignHashValidator : AbstractValidator<SignHashCommand>
        {
            public SignHashValidator()
            {
                RuleFor(s => s.Pin2).NotEmpty().WithMessage("من فضلك لتوقيع الملفات ادخل رمز PIN2 الخاص بك.");
            }
        }

        #endregion

        #region Handler

        public class IsCorrectPin2QueryHandler : IRequestHandler<IsCorrectPin2Query, bool>
        {
            private readonly IEgidDbContext _context;
            private readonly ICurrentUserService _currentUser;
            private readonly ICardManagerService _cardManager;

            public IsCorrectPin2QueryHandler(IEgidDbContext context,
                ICurrentUserService currentUser, ICardManagerService cardManager)
            {
                _context = context;
                _currentUser = currentUser;
                _cardManager = cardManager;
            }

            public async Task<bool> Handle(IsCorrectPin2Query request, CancellationToken cancellationToken)
            {
                var card = await _context.Cards.FindAsync(_currentUser.CardId);

                if (card is null) throw new EntityNotFoundException("Card", _currentUser.CardId);

                return _cardManager.VerifyPin2(card, request.Pin2);
            }
        }

        #endregion
    }
}
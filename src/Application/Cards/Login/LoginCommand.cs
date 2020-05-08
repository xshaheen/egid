using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common.Exceptions;
using EGID.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

namespace EGID.Application.Cards.Login
{
    public class LoginCommand : IRequest<string>
    {
        public string CardId { get; set; }

        public string Pin1 { get; set; }

        #region Validator

        public class LoginValidator : AbstractValidator<LoginCommand>
        {
            public LoginValidator()
            {
                RuleFor(c => c.Pin1).NotEmpty().WithMessage("من فضلك ادخل رمز Pin1 لتسجيل الدخول.");
                RuleFor(c => c.CardId).NotEmpty();
            }
        }

        #endregion

        #region Handler

        public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
        {
            private readonly ICardManagerService _cardManager;

            public LoginCommandHandler(ICardManagerService cardManager) => _cardManager = cardManager;

            public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                var (result, token) = await _cardManager.LoginAsync(request.CardId, request.Pin1);

                if (result.Failed) throw new BadRequestException(result.Errors);

                return token;
            }
        }

        #endregion
    }
}
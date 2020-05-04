using System.Threading;
using System.Threading.Tasks;
using EGID.Common.Exceptions;
using FluentValidation;
using MediatR;

namespace EGID.Application.Cards.Commands
{
    public class ChangePukCommand : IRequest
    {
        public string CardId { get; set; }
        public string CurrentPuk { get; set; }
        public string NewPuk { get; set; }
    }

    #region Validator

    public class ChangePukValidator : AbstractValidator<ChangePukCommand>
    {
        public ChangePukValidator()
        {
            RuleFor(c => c.CardId)
                .NotNull().WithMessage("من فضلك اضف كود المواطن للطلب.")
                .Length(128).WithMessage("كود البطاقة غير صحيح.");

            RuleFor(c => c.CurrentPuk)
                .NotEmpty().WithMessage("من فضلك ادخل رمز PuK الحالي.")
                .Length(6, 128).WithMessage("رمز Puk يجب ان تكون بين 6 الي 128 حرف.");

            RuleFor(c => c.NewPuk)
                .NotEmpty().WithMessage("من فضلك ادخل رمز PuK الجديد.")
                .Length(6, 128).WithMessage("رمز Puk يجب ان تكون بين 6 الي 128 حرف.")
                .NotEqual(c => c.CurrentPuk).WithMessage("رمز PUk الجديدة مطابقة للحالي من فضلك اعد المحاولة مع رمز جديد. ");
        }
    }

    #endregion

    #region Handler

    public class ChangePukHandler : IRequestHandler<ChangePukCommand>
    {
        private readonly ICardManagerService _cardManager;

        public ChangePukHandler(ICardManagerService cardManager) => _cardManager = cardManager;

        public async Task<Unit> Handle(ChangePukCommand request, CancellationToken cancellationToken)
        {
            var result = await _cardManager.ChangePukAsync(request.CardId, request.CurrentPuk, request.NewPuk);

            if (result.Failed) throw new BadRequestException(result.Errors);

            return Unit.Value;
        }
    }

    #endregion
}

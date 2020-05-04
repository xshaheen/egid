using System.Threading;
using System.Threading.Tasks;
using EGID.Common.Exceptions;
using FluentValidation;
using MediatR;

namespace EGID.Application.Cards.Commands
{
    public class ChangePin2Command : IRequest
    {
        public string CardId { get; set; }
        public string Puk { get; set; }
        public string NewPin2 { get; set; }
    }

    #region Validator

    public class ChangePin2Validator : AbstractValidator<ChangePin2Command>
    {
        public ChangePin2Validator()
        {
            RuleFor(c => c.CardId)
                .NotNull().WithMessage("من فضلك اضف كود المواطن للطلب.")
                .Length(128).WithMessage("كود البطاقة غير صحيح.");

            RuleFor(c => c.Puk)
                .NotEmpty().WithMessage("من فضلك ادخل رمز PuK الحالي.")
                .Length(6, 128).WithMessage("رمز Puk يجب ان تكون بين 6 الي 128 حرف.");

            RuleFor(c => c.NewPin2)
                .NotEmpty().WithMessage("من فضلك ادخل رمز Pin2 الجديد.")
                .Length(6, 128).WithMessage("رمز Pin2 يجب ان تكون بين 6 الي 128 حرف.")
                .NotEqual(c => c.Puk).WithMessage("رمز Pin2 الجديدة لا يجب ان يكون مطابق لرمز Puk من فضلك اعد المحاولة مع رمز جديد.");
        }
    }

    #endregion

    #region Handler

    public class ChangePin2Handler : IRequestHandler<ChangePin2Command>
    {
        private readonly ICardManagerService _cardManager;

        public ChangePin2Handler(ICardManagerService cardManager) => _cardManager = cardManager;

        public async Task<Unit> Handle(ChangePin2Command request, CancellationToken cancellationToken)
        {
            var result = await _cardManager.ChangePin2Async(request.CardId, request.Puk, request.NewPin2);

            if (result.Failed) throw new BadRequestException(result.Errors);

            return Unit.Value;
        }
    }

    #endregion
}

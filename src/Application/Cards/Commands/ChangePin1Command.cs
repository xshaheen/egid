using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common.Exceptions;
using EGID.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

namespace EGID.Application.Cards.Commands
{
    public class ChangePin1Command : IRequest
    {
        [Required] public string CardId { get; set; }
        [Required] public string Puk { get; set; }
        [Required] public string NewPin1 { get; set; }
    }

    #region Validator

    public class ChangePin1Validator : AbstractValidator<ChangePin1Command>
    {
        public ChangePin1Validator()
        {
            RuleFor(c => c.CardId)
                .NotEmpty().WithMessage("من فضلك اضف كود المواطن للطلب.");

            RuleFor(c => c.Puk)
                .NotEmpty().WithMessage("من فضلك ادخل رمز PuK الحالي.")
                .Length(6, 128).WithMessage("رمز Puk يجب ان تكون بين 6 الي 128 حرف.");

            RuleFor(c => c.NewPin1)
                .NotEmpty().WithMessage("من فضلك ادخل رمز Pin2 الجديد.")
                .Length(6, 128).WithMessage("رمز Pin1 يجب ان تكون بين 6 الي 128 حرف.")
                .NotEqual(c => c.Puk)
                .WithMessage("رمز Pin1 الجديدة لا يجب ان يكون مطابق لرمز Puk من فضلك اعد المحاولة مع رمز جديد.");
        }
    }

    #endregion

    #region Handler

    public class ChangePin1Handler : IRequestHandler<ChangePin1Command>
    {
        private readonly ICardManagerService _cardManager;

        public ChangePin1Handler(ICardManagerService cardManager) => _cardManager = cardManager;

        public async Task<Unit> Handle(ChangePin1Command request, CancellationToken cancellationToken)
        {
            var result = await _cardManager.ChangePin1Async(request.CardId, request.Puk, request.NewPin1);

            if (result.Failed) throw new BadRequestException(result.Errors);

            return Unit.Value;
        }
    }

    #endregion
}
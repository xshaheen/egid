using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common;
using FluentValidation;
using MediatR;

namespace EGID.Application.Cards.Commands
{
    public class CreateCardCommand : IRequest
    {
        public string OwnerId { get; set; }

        public string Puk { get; set; }

        public string Pin1 { get; set; }

        public string Pin2 { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        #region Validator

        public class CreateCardValidator : AbstractValidator<CreateCardCommand>
        {
            public CreateCardValidator()
            {
                RuleFor(x => x.OwnerId)
                    .NotNull().WithMessage("من فضلك اضف كود المواطن للطلب.")
                    .Length(128).WithMessage("كود البطاقة غير صحيح.");

                RuleFor(x => x.Email).EmailAddress()
                    .WithMessage("البريد الاليكتروني غير صحيح.");

                RuleFor(x => x.PhoneNumber).MaximumLength(24)
                    .WithMessage("رقم الموبايل لا يمكن ان يكون اكثر من 24 رقم.")
                    .Matches(Regexes.InternationalPhone)
                    .WithMessage("رقم الهاتف غير صحيح من فضلك تحقق من رقم الهاتف واعد المحاولة.");

                RuleFor(x => x.Puk)
                    .NotEmpty().WithMessage("من فضلك اضف Puk الخاص بالمواطن.")
                    .MaximumLength(128).WithMessage("Puk لا يجب ان يكون اقل من 128 حرف.");

                RuleFor(x => x.Pin1)
                    .NotEmpty().WithMessage("من فضلك اضف PIN1 الخاص بالمواطن.")
                    .MaximumLength(128).WithMessage("Pin1 لا يجب ان يكون اقل من 128 حرف.");

                RuleFor(x => x.Pin2)
                    .NotEmpty().WithMessage("من فضلك اضف Pin2 الخاص بالمواطن.")
                    .MaximumLength(128).WithMessage("Pin2 لا يجب ان يكون اقل من 128 حرف.");
            }
        }

        #endregion

        #region Handler

        public class AddCardCommandHandler : IRequestHandler<CreateCardCommand>
        {
            private readonly ICardManagerService _cardManager;

            public AddCardCommandHandler(ICardManagerService cardManager) => _cardManager = cardManager;

            public async Task<Unit> Handle(CreateCardCommand request, CancellationToken _)
            {
                await _cardManager.RegisterAsync(request);

                return Unit.Value;
            }
        }

        #endregion
    }
}
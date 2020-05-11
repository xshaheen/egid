using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common;
using EGID.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

namespace EGID.Application.Cards.Commands
{
    public class CreateCardCommand : IRequest<string>
    {
        [Required] public string OwnerId { get; set; }

        [Required] public string Puk { get; set; }
        [Required] public string Pin1 { get; set; }
        [Required] public string Pin2 { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        #region Validator

        public class CreateCardValidator : AbstractValidator<CreateCardCommand>
        {
            public CreateCardValidator()
            {
                RuleFor(x => x.OwnerId)
                    .NotEmpty().WithMessage("الكود غير مرفق مع الطلب.");

                RuleFor(x => x.Email).EmailAddress()
                    .WithMessage("البريد الاليكتروني غير صحيح.");

                RuleFor(x => x.PhoneNumber).MaximumLength(24)
                    .WithMessage("رقم الموبايل لا يمكن ان يكون اكثر من 24 رقم.")
                    .Matches(Regexes.InternationalPhone).Unless(x => x.PhoneNumber is null)
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

        public class AddCardCommandHandler : IRequestHandler<CreateCardCommand, string>
        {
            private readonly ICardManagerService _cardManager;

            public AddCardCommandHandler(ICardManagerService cardManager) => _cardManager = cardManager;

            public async Task<string> Handle(CreateCardCommand request, CancellationToken _)
            {
                var (_, id) = await _cardManager.RegisterAsync(request);

                return id;
            }
        }

        #endregion
    }
}
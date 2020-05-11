using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common;
using EGID.Application.Common.Exceptions;
using EGID.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

namespace EGID.Application.CivilAffairs.Commands
{
    public class AddEmployeeCommand : IRequest
    {
        [Required] public string CardId { get; set; }

        #region Validator

        public class AddEmployeeValidator : AbstractValidator<AddEmployeeCommand>
        {
            public AddEmployeeValidator()
            {
                RuleFor(x => x.CardId)
                    .NotEmpty().WithMessage("الكود غير مرفق مع الطلب.");
            }
        }

        #endregion

        #region Handler

        public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand>
        {
            private readonly ICardManagerService _cardManager;

            public AddEmployeeCommandHandler(ICardManagerService cardManager) => _cardManager = cardManager;

            public async Task<Unit> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
            {
                var result = await _cardManager.AddToRoleAsync(request.CardId, Roles.CivilAffairs);

                if (result.Failed) throw new BadRequestException(result.Errors);

                return Unit.Value;
            }
        }

        #endregion
    }
}
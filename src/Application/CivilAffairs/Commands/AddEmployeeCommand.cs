using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common;
using EGID.Application.Common.Exceptions;
using EGID.Application.Common.Interfaces;
using MediatR;

namespace EGID.Application.CivilAffairs.Commands
{
    public class AddEmployeeCommand : IRequest
    {
        public string CardId { get; set; }

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
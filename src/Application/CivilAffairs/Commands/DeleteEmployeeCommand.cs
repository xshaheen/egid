using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common;
using EGID.Application.Common.Exceptions;
using EGID.Application.Common.Interfaces;
using MediatR;

namespace EGID.Application.CivilAffairs.Commands
{
    public class DeleteEmployeeCommand : IRequest
    {
        public string CardId { get; set; }

        #region Handler

        public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
        {
            private readonly ICardManagerService _cardManager;

            public DeleteEmployeeCommandHandler(ICardManagerService cardManager) => _cardManager = cardManager;

            public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
            {
                var result = await _cardManager.RemoveFromRoleAsync(request.CardId, Roles.CivilAffairs);

                if (result.Failed) throw new BadRequestException(result.Errors);

                return Unit.Value;
            }
        }

        #endregion
    }
}
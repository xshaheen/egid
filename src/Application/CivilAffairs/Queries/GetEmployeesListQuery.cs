using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common;
using EGID.Application.Common.Interfaces;
using MediatR;

namespace EGID.Application.CivilAffairs.Queries
{
    public class GetEmployeesListQuery : IRequest<IList<EmployeesVm>>
    {
        public class GetEmployeesListHandler : IRequestHandler<GetEmployeesListQuery, IList<EmployeesVm>>
        {
            private readonly ICardManagerService _cardManager;

            public GetEmployeesListHandler(ICardManagerService cardManager) => _cardManager = cardManager;

            public async Task<IList<EmployeesVm>> Handle(GetEmployeesListQuery request, CancellationToken cancellationToken)
            {
                var employeeCards = await _cardManager.InRole(Roles.CivilAffairs);

                return employeeCards;
            }
        }
    }
}
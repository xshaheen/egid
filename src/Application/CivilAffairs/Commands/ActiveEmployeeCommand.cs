using System;
using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common.Interfaces;
using MediatR;

namespace EGID.Application.CivilAffairs.Commands
{
    public class ActiveEmployeeCommand : IRequest
    {
        public string CardId { get; set; }

        #region Handler

        public class ActiveEmployeeCommandHandler : IRequestHandler<ActiveEmployeeCommand>
        {
            private readonly IEgidDbContext _context;

            public ActiveEmployeeCommandHandler(IEgidDbContext context) => _context = context;

            public Task<Unit> Handle(ActiveEmployeeCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
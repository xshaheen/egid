using System;
using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

namespace EGID.Application.Health.Commands
{
    public class UpdateHealthInfoCommand : IRequest
    {
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }

        #region Validator

        public class UpdateHealthInfoValidator : AbstractValidator<UpdateHealthInfoCommand>
        {
            public UpdateHealthInfoValidator() { }
        }

        #endregion

        #region Handler

        public class UpdateHealthInfoHandler : IRequestHandler<UpdateHealthInfoCommand>
        {
            private readonly IEgidDbContext _context;

            public UpdateHealthInfoHandler(IEgidDbContext context) => _context = context;

            public Task<Unit> Handle(UpdateHealthInfoCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common.Exceptions;
using EGID.Application.Common.Interfaces;
using EGID.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            private readonly ICurrentUserService _currentUser;

            public UpdateHealthInfoHandler(IEgidDbContext context, ICurrentUserService currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public async Task<Unit> Handle(UpdateHealthInfoCommand request, CancellationToken cancellationToken)
            {
                var card = await _context.Cards
                    .Include(c => c.Citizen)
                    .ThenInclude(c => c.HealthInfo)
                    .FirstOrDefaultAsync(c => c.Id == _currentUser.UserId, cancellationToken);

                if (card?.Citizen?.HealthInfo is null) throw new EntityNotFoundException(nameof(Card), _currentUser.UserId);

                // TODO
                return Unit.Value;
            }
        }

        #endregion
    }
}
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EGID.Common.Exceptions;
using EGID.Common.Models.Result;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EGID.Application.CitizenDetails.Commands
{
    public class DeleteCitizenCommand : IRequest
    {
        public string Id { get; set; }

        #region Validator

        public class DeleteCitizenValidator : AbstractValidator<DeleteCitizenCommand>
        {
            public DeleteCitizenValidator()
            {
                RuleFor(x => x.Id).NotEmpty().Length(128);
            }
        }

        #endregion

        #region Handler

        public class DeleteCitizenCommandHandler : IRequestHandler<DeleteCitizenCommand>
        {
            private readonly IEgidDbContext _context;
            private readonly ICardManagerService _cardManager;

            public DeleteCitizenCommandHandler(IEgidDbContext context, ICardManagerService cardManager)
            {
                _context = context;
                _cardManager = cardManager;
            }

            public async Task<Unit> Handle(DeleteCitizenCommand request, CancellationToken cancellationToken)
            {
                var citizen = await _context.CitizenDetails.FindAsync(request.Id);

                if (citizen is null) throw new EntityNotFoundException("Citizen", request.Id);

                var result = Result.Success();

                try
                {
                    result = await _cardManager.DeleteAsync(citizen.CardId);
                }
                catch (EntityNotFoundException) { }

                if (result.Failed) throw new DbUpdateException(result.Errors.FirstOrDefault() ?? string.Empty);

                _context.CitizenDetails.Remove(citizen);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }

        #endregion
    }
}
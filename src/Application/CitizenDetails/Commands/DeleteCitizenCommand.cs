using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EGID.Application.CitizenDetails.Commands
{
    public class DeleteCitizenCommand : IRequest
    {
        public string Id { get; set; }

        public class DeleteCitizenCommandHandler : IRequestHandler<DeleteCitizenCommand>
        {
            private readonly IEgidDbContext _context;

            public DeleteCitizenCommandHandler(IEgidDbContext context) => _context = context;

            public async Task<Unit> Handle(DeleteCitizenCommand request, CancellationToken cancellationToken)
            {
                var citizen = await _context.CitizenDetails.FindAsync(request.Id);

                _context.CitizenDetails.Remove(citizen);

                try
                {
                    await _context.SaveChangesAsync(cancellationToken);
                }
                catch (Exception)
                {
                    throw new DbUpdateException();
                }

                return Unit.Value;
            }
        }
    }

    public class DeleteCitizenValidator : AbstractValidator<DeleteCitizenCommand>
    {
        public DeleteCitizenValidator()
        {
            RuleFor(x => x.Id).NotEmpty().Length(128);
        }
    }
}
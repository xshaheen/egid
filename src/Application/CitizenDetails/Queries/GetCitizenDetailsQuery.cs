using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common.Exceptions;
using EGID.Application.Common.Interfaces;
using EGID.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EGID.Application.CitizenDetails.Queries
{
    public class GetCitizenDetailsQuery : IRequest<CitizenDetailsVm>
    {
        public string Id { get; set; }

        #region Handler

        public class GetCitizenDetailsQueryHandler : IRequestHandler<GetCitizenDetailsQuery, CitizenDetailsVm>
        {
            private readonly IEgidDbContext _context;

            public GetCitizenDetailsQueryHandler(IEgidDbContext context) => _context = context;

            public async Task<CitizenDetailsVm> Handle(
                GetCitizenDetailsQuery request,
                CancellationToken cancellationToken
            )
            {
                var citizen = await _context.CitizenDetails
                    .Select(c => new CitizenDetailsVm
                    {
                        Id = c.Id,
                        FatherId = c.FatherId,
                        MotherId = c.MotherId,
                        FullName = c.FullName,
                        Address = c.Address,
                        Gender = c.Gender,
                        Religion = c.Religion,
                        SocialStatus = c.SocialStatus,
                        DateOfBirth = c.DateOfBirth,
                        PhotoUrl = c.PhotoUrl,
                        Create = c.Create,
                        CreateBy = c.CreateBy,
                        LastModified = c.LastModified,
                        LastModifiedBy = c.LastModifiedBy
                    })
                    .Where(x => x.Id == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);

                if (citizen is null) throw new EntityNotFoundException(nameof(CitizenDetail), request.Id);

                return citizen;
            }
        }

        #endregion
    }
}
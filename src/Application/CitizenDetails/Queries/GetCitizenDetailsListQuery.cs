using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EGID.Application.CitizenDetails.Queries
{
    public class GetCitizenDetailsListQuery : IRequest<List<CitizenDetailsVm>>
    {
        #region Handler

        public class GetCitizenDetailsQueryHandler : IRequestHandler<GetCitizenDetailsListQuery, List<CitizenDetailsVm>>
        {
            private readonly IEgidDbContext _context;

            public GetCitizenDetailsQueryHandler(IEgidDbContext context) => _context = context;

            public async Task<List<CitizenDetailsVm>> Handle(
                GetCitizenDetailsListQuery request, CancellationToken cancellationToken
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
                    .OrderBy(c => c.Create)
                    .ToListAsync(cancellationToken);

                return citizen;
            }
        }

        #endregion
    }
}
using System.Threading.Tasks;
using EGID.Domain.Entities;

namespace EGID.Application.Citizens
{
    public interface ICitizenService
    {
        #region Commands

        Task AddCitizen();

        Task UpdateCitizen();

        #endregion Commands

        #region Query

        Task<CitizenDetails> GetByIdAsync(string id);

        #endregion Query
    }

    class CitizenService : ICitizenService
    {

        private readonly IEgidDbContext _context;

        public CitizenService(IEgidDbContext context)
        {
            _context = context;
        }

        public Task AddCitizen()
        {



            throw new System.NotImplementedException();
        }

        public Task UpdateCitizen() => throw new System.NotImplementedException();

        public Task<CitizenDetails> GetByIdAsync(string id) => throw new System.NotImplementedException();
    }
}

using System.Threading.Tasks;
using EGID.Domain.Entities;

namespace EGID.Data.Citizens
{
    public interface ICitizenService
    {
        #region Commands

        Task AddCitizen();

        Task UpdateCitizen();

        #endregion Commands

        #region Query

        Task<Citizen> GetByIdAsync(string id);

        #endregion Query
    }
}

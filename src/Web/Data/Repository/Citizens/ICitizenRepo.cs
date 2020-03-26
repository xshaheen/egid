using System.Threading.Tasks;
using EGID.Web.Entities;

namespace EGID.Web.Data.Repository.Citizens
{
    public interface ICitizenRepo
    {
        #region Commands

        Task AddCitizen(AddCitizenModel model);

        Task UpdateCitizen(AddCitizenModel model);

        #endregion

        #region Query

        Task<Citizen> GetByIdAsync(string id);

        #endregion
    }
}

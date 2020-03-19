using System.Threading.Tasks;
using EGID.Data.Entities;

namespace EGID.Data.Repository
{
    public interface ICitizenRepo
    {
        Task<Citizen> GetByIdAsync(string id);
    }
}

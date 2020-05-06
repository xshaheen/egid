using System.Threading.Tasks;
using EGID.Common.Models.Result;

namespace EGID.Application.Common.Interfaces
{
    public interface IRoleManagerService
    {
        /// <summary>
        ///     Is there exit any roles in database. true if exist, false otherwise.
        /// </summary>
        Task<bool> AnyAsync { get; }

        /// <summary>
        ///     Create a new role and save to database.
        /// </summary>
        Task<Result> CreateRoleAsync(string roleName);
    }
}
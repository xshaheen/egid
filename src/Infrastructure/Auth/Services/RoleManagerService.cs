using System.Threading.Tasks;
using EGID.Application;
using EGID.Common.Models.Result;
using EGID.Infrastructure.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EGID.Infrastructure.Auth.Services
{
    public class RoleManagerService : IRoleManagerService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleManagerService(RoleManager<IdentityRole> roleManager) => _roleManager = roleManager;

        public Task<bool> AnyAsync => _roleManager.Roles.AnyAsync();

        public async Task<Result> CreateRoleAsync(string roleName)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            return result.ToResult();
        }
    }
}

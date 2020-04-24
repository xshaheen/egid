using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EGID.Data
{
    public class DataSeed
    {
        public static void CreateRoles(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            if (roleManager.Roles.Any()) return;

            var roles = Roles.GetRoles();
        }

        public static void Admin(IServiceProvider services, IConfiguration config)
        {
            var username = config.GetSection("AdministratorIdentity")["username"];
            var password = config.GetSection("AdministratorIdentity")["password"];
        }

        public Task SeedCitizen()
        {
            return null;
        }
    }
}

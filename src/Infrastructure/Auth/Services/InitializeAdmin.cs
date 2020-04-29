// using System.Threading;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Internal;
//
// namespace EGID.Infrastructure.Auth.Services
// {
//     public class InitializeAdmin
//     {
//         private readonly UserManager<Card> _userManager;
//         private readonly RoleManager<IdentityRole> _roleManager;
//
//         public InitializeAdmin(
//             UserManager<Card> userManager,
//             RoleManager<IdentityRole> roleManager
//         )
//         {
//             _roleManager = roleManager;
//             _userManager = userManager;
//         }
//
//         public async Task Initial()
//         {
//             await InitializeSettings();
//             await InitializeRoles();
//             await InitializeAdmin();
//         }
//
//         private async Task InitializeAdmin()
//         {
//             if (await _userManager.Users.AnyAsync()) return;
//
//             await _userManagerAsync("admin@admin.com", "P@ssw0rd");
//         }
//
//         private async Task InitializeRoles()
//         {
//             if (await _roleManager.Roles.AnyAsync()) return;
//
//             await _roleManager.CreateAsync(new IdentityRole("Admin"));
//             await _roleManager.CreateAsync(new IdentityRole("CitizenAffairEmployee"));
//             await _roleManager.CreateAsync(new IdentityRole(""));
//         }
//
//     }
// }
//
// }
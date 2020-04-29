using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace EGID.Application.DbInitializer
{
    public class InitializeDbCommand : IRequest
    {
        public class InitializeDbHandler : IRequestHandler<InitializeDbCommand>
        {
            private readonly IEgidDbContext _context;
            private readonly IRoleManagerService _roleManager;
            private readonly ICardManagerService _userManager;

            public InitializeDbHandler(
                IEgidDbContext context,
                ICardManagerService userManager,
                IRoleManagerService roleManager
            )
            {
                _context = context;
                _roleManager = roleManager;
                _userManager = userManager;
            }

            public async Task<Unit> Handle(InitializeDbCommand request, CancellationToken _)
            {
                await InitializeRoles();
                await InitializeAdmin();

                return Unit.Value;
            }

            private async Task InitializeRoles()
            {
                if (await _roleManager.AnyAsync) return;

                await _roleManager.CreateRoleAsync("Admin");
                await _roleManager.CreateRoleAsync("CivilAffair");
                await _roleManager.CreateRoleAsync("Doctor");
            }

            private async Task InitializeAdmin()
            {
                if (await _userManager.AnyAsync) return;

                // await _userManager.CreateUserAsync("admin@admin.com", "P@ssw0rd");
            }

        }
    }
}

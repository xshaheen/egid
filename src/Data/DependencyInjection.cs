using EGID.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EGID.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<EgidDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IEgidDbContext>(provider => provider.GetService<EgidDbContext>());

            return services;
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using EGID.Application.DbInitializer;
using EGID.Infrastructure.Data;
using EGID.Infrastructure.Security.Jwt;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EGID.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scoped = host.Services.CreateScope())
            {
                var services = scoped.ServiceProvider;

                try
                {
                    // Create database if not already exist or apply any new migration

                    var appDbContext = services.GetService<EgidDbContext>();
                    await appDbContext.Database.MigrateAsync();

                    var mediator = services.GetService<IMediator>();

                    await mediator.Send(new InitializeDbCommand(), CancellationToken.None);
                }
                catch (Exception e)
                {
                    var logger = services.GetService<ILogger<Program>>();
                    logger.LogError(e, "An error occured while migrating or initializing the database.");
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}
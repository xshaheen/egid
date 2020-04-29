using System.Text.Json.Serialization;
using EGID.Application;
using EGID.Data;
using EGID.Infrastructure;
using EGID.Web.Infrastructure.Middleware.CustomExceptionHandlerMiddleware;
using EGID.Web.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EGID.Web
{
    public class Startup
    {
        public Startup(IConfiguration config) => Configuration = config;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(Configuration);
            services.AddDatabase(Configuration);

            services.AddHttpContextAccessor();

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = false;
            });

            services.AddControllers()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.IgnoreNullValues = true;
                    // Use string enums in the serializer
                    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                }).AddFluentValidation(config =>
                    config.RegisterValidatorsFromAssemblyContaining<IEgidDbContext>());

            services.AddTransient<IFilesDirectoryService, FilesDirectoryService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddSpaStaticFiles(config => { config.RootPath = "ClientApp/dist"; });

            services.AddOpenApiDocument(document =>
            {
                document.DocumentName = "specification";
                document.PostProcess = d =>
                {
                    d.Info.Title = "EGID API";
                    d.Info.Version = "0.0.1";
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDatabaseErrorPage();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseCustomExceptionHandler();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            if (!env.IsDevelopment()) app.UseSpaStaticFiles();

            app.UseOpenApi();
            app.UseReDoc(
                config =>
                {
                    config.Path = "/api";
                    config.DocumentPath = "/api/specification.json";
                });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment()) spa.UseAngularCliServer("start");
            });
        }
    }
}
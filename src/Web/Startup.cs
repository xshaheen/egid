using System.Text.Json.Serialization;
using EGID.Application;
using EGID.Application.Common.Interfaces;
using EGID.Application.Common.Models.Files;
using EGID.Infrastructure;
using EGID.Web.Infrastructure;
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
            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddTransient<IFilesDirectoryService, FilesDirectoryService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = false;
            });

            services.AddControllers(config => config.Filters.Add<ApiExceptionFilter>())
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.IgnoreNullValues = true;
                    // Use string enums in the serializer
                    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                }).AddFluentValidation(config =>
                    config.RegisterValidatorsFromAssemblyContaining<IEgidDbContext>());

            services.AddSpaStaticFiles(config => { config.RootPath = "ClientApp/dist"; });

            services.AddOpenApiDocument(document =>
            {
                document.DocumentName = "specification";
                document.Title = "EGID API";
                document.GenerateEnumMappingDescription = true;
                document.ExcludedTypeNames = new[] { nameof(BinaryFile) };
                document.Description = "EGID API specification";
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
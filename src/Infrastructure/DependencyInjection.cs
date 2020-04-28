using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using EGID.Common.Interfaces;
using EGID.Infrastructure.Auth;
using EGID.Infrastructure.Auth.Services;
using EGID.Infrastructure.Crypto;
using EGID.Infrastructure.DigitalSignature;
using EGID.Infrastructure.KeysGenerator;
using EGID.Infrastructure.Time;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace EGID.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IKeyGeneratorService, KeyGeneratorService>();
            services.AddTransient<IAesCryptoService, AesCryptoService>();
            services.AddTransient<IDigitalSignatureService, DigitalSignatureService>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<Card, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = true;
                    options.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 0, 5, 0);
                })
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            // remove default claims
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(config =>
                {
                    config.RequireHttpsMetadata = true;
                    config.SaveToken = true;
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"])),
                        ValidIssuer = configuration["JwtIssuer"],
                        ValidAudience = configuration["JwtIssuer"],
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddScoped<PrivateKeyOptions>();
            services.AddTransient<IDateTime, UtcDateTime>();

            return services;
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EGID.Infrastructure.Auth
{
    public class AuthDbContext : IdentityDbContext<Card>
    {
        public AuthDbContext() { }

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=egid_db;Trusted_Connection=True;MultipleActiveResultSets=true"
            );

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);

            builder.Entity<Card>().ToTable("EGCards");
            builder.Entity<IdentityRole>().ToTable("EGRoles");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("EGRoleClaim");
            builder.Entity<IdentityUserRole<string>>().ToTable("EGUserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("EGUserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("EGUserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("EGUserToken");
        }
    }
}
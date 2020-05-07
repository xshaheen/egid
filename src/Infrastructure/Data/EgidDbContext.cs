using System.Threading;
using System.Threading.Tasks;
using EGID.Application.Common.Interfaces;
using EGID.Domain;
using EGID.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EGID.Infrastructure.Data
{
    public class EgidDbContext : IdentityDbContext<Card>, IEgidDbContext
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IDateTime _dateTime;

        public EgidDbContext() { }

        public EgidDbContext(DbContextOptions<EgidDbContext> options) : base(options) { }

        public EgidDbContext(
            DbContextOptions<EgidDbContext> options,
            ICurrentUserService currentUser,
            IDateTime dateTime
        ) : base(options)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
        }

        public virtual DbSet<Card> Cards { get; set; }

        public virtual DbSet<CitizenDetail> CitizenDetails { get; set; }

        public virtual DbSet<DeathCertificate> DeathCertificates { get; set; }

        public virtual DbSet<HealthInfo> HealthInformation { get; set; }

        public virtual DbSet<HealthRecord> HealthRecords { get; set; }
        public virtual DbSet<HealthRecordAttachment> HealthRecordAttachments { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Create = _dateTime.Now;
                        entry.Entity.CreateBy = _currentUser.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.Now;
                        entry.Entity.LastModifiedBy = _currentUser.UserId;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(EgidDbContext).Assembly);

            builder.Entity<Card>().ToTable("EGCards");
            builder.Entity<IdentityRole>().ToTable("EGRoles");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("EGRoleClaim");
            builder.Entity<IdentityUserRole<string>>().ToTable("EGUserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("EGUserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("EGUserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("EGUserToken");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=egid_db;Trusted_Connection=True;MultipleActiveResultSets=true"
            );

            base.OnConfiguring(optionsBuilder);
        }
    }
}
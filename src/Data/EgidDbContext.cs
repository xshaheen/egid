using System.Threading;
using System.Threading.Tasks;
using EGID.Application;
using EGID.Common.Interfaces;
using EGID.Domain;
using EGID.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EGID.Data
{
    public class EgidDbContext : DbContext, IEgidDbContext
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

        public virtual DbSet<CitizenDetails> CitizenDetails { get; set; }

        public virtual DbSet<DeathCertificate> DeathCertificates { get; set; }

        public virtual DbSet<HealthInfo> HealthInformation { get; set; }

        public virtual DbSet<ExitHospitalRecord> ExitHospitalRecords { get; set; }
        public virtual DbSet<ExitHospitalRecordAttachment> ExitHospitalRecordAttachments { get; set; }

        public virtual DbSet<HealthRecord> HealthRecords { get; set; }
        public virtual DbSet<HealthRecordAttachment> HealthRecordAttachments { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EgidDbContext).Assembly);
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
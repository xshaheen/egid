using System.Threading;
using System.Threading.Tasks;
using EGID.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EGID.Application
{
    public interface IEgidDbContext
    {
        DbSet<CitizenDetails> CitizenDetails { get; set; }

        DbSet<DeathCertificate> DeathCertificates { get; set; }

        DbSet<HealthInfo> HealthInformation { get; set; }

        DbSet<ExitHospitalRecord> ExitHospitalRecords { get; set; }
        DbSet<ExitHospitalRecordAttachment> ExitHospitalRecordAttachments { get; set; }

        DbSet<HealthRecord> HealthRecords { get; set; }
        DbSet<HealthRecordAttachment> HealthRecordAttachments { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}

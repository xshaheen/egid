using System.Threading;
using System.Threading.Tasks;
using EGID.Domain;
using EGID.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EGID.Application.Common.Interfaces
{
    public interface IEgidDbContext
    {
        DbSet<Card> Cards { get; set; }

        DbSet<CitizenDetail> CitizenDetails { get; set; }

        DbSet<DeathCertificate> DeathCertificates { get; set; }

        DbSet<HealthInfo> HealthInformation { get; set; }

        DbSet<HealthRecord> HealthRecords { get; set; }
        DbSet<HealthRecordAttachment> HealthRecordAttachments { get; set; }

        /// <summary>
        ///     This method above of saving changes to database it delegate to
        ///     it to handle properties of <see cref="AuditableEntity"/>
        ///     properties for the derived types.
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
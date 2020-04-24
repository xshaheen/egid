using EGID.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EGID.Data.Configuration
{
    public class HealthInfoConfiguration : IEntityTypeConfiguration<HealthInfo>
    {
        public void Configure(EntityTypeBuilder<HealthInfo> builder)
        {
            builder.Property(e => e.Id).HasMaxLength(128);
            builder.Property(e => e.CitizenId).IsRequired().HasMaxLength(128);

            builder.Property(e => e.Phone1).HasMaxLength(24);
            builder.Property(e => e.Phone2).HasMaxLength(24);
            builder.Property(e => e.Phone3).HasMaxLength(24);
        }
    }
}

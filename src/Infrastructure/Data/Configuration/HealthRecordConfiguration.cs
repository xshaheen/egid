using EGID.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EGID.Infrastructure.Data.Configuration
{
    public class HealthRecordConfiguration : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.Property(e => e.Id).HasMaxLength(128);
            builder.Property(e => e.HealthInfoId).IsRequired().HasMaxLength(128);

            builder.Property(e => e.CreateBy).IsRequired().HasMaxLength(128);
            builder.Property(e => e.LastModifiedBy).HasMaxLength(128);
            builder.Property(e => e.LastModified).HasColumnType("datetime");

            builder.Property(e => e.Medications).HasMaxLength(4096);
            builder.Property(e => e.Diagnosis).HasMaxLength(4096);

            builder.HasOne(e => e.HealthInfo)
                .WithMany(e => e.HealthRecords)
                .HasForeignKey(e => e.HealthInfoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_HealthRecords_HealthInfo_HealthInfoId");
        }
    }
}
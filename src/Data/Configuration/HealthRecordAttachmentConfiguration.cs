using EGID.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EGID.Data.Configuration
{
    public class HealthRecordAttachmentConfiguration : IEntityTypeConfiguration<HealthRecordAttachment>
    {
        public void Configure(EntityTypeBuilder<HealthRecordAttachment> builder)
        {
            builder.Property(e => e.Id).HasMaxLength(128);

            builder.Property(e => e.HealthRecordId).IsRequired().HasMaxLength(128);

            builder.HasOne(e => e.HealthRecord)
                .WithMany(e => e.Attachments)
                .HasForeignKey(e => e.HealthRecordId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_HealthRecordAttachment_HealthRecord_HealthRecordId");
        }
    }
}

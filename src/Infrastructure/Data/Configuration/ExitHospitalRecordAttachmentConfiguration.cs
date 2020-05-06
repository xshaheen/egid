using EGID.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EGID.Infrastructure.Data.Configuration
{
    public class ExitHospitalRecordAttachmentConfiguration : IEntityTypeConfiguration<ExitHospitalRecordAttachment>
    {
        public void Configure(EntityTypeBuilder<ExitHospitalRecordAttachment> builder)
        {
            builder.Property(e => e.Id).HasMaxLength(128);

            builder.Property(e => e.ExitHospitalRecordId).IsRequired().HasMaxLength(128);

            builder.HasOne(e => e.ExitHospitalRecord)
                .WithMany(e => e.Attachments)
                .HasForeignKey(e => e.ExitHospitalRecordId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ExitHospitalRecordAttachment_ExitHospitalRecord_ExitHospitalRecordId");
        }
    }
}
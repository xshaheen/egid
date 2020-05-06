using EGID.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EGID.Infrastructure.Data.Configuration
{
    public class DeathCertificateConfiguration : IEntityTypeConfiguration<DeathCertificate>
    {
        public void Configure(EntityTypeBuilder<DeathCertificate> builder)
        {
            builder.Property(e => e.Id).HasMaxLength(128);
            builder.Property(e => e.CitizenId).IsRequired().HasMaxLength(128);

            builder.Property(e => e.CreateBy).IsRequired().HasMaxLength(128);
            builder.Property(e => e.LastModifiedBy).HasMaxLength(128);
            builder.Property(e => e.LastModified).HasColumnType("datetime");

            builder.Property(e => e.CauseOfDeath).HasMaxLength(4098);

            builder.HasOne(e => e.Citizen)
                .WithOne(e => e.DeathCertificate)
                .HasForeignKey<DeathCertificate>(e => e.CitizenId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DeathCertificate_Citizen_CitizenId");
        }
    }
}
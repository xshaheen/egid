using EGID.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EGID.Data.Configuration
{
    public class CitizenDetailsConfiguration : IEntityTypeConfiguration<CitizenDetail>
    {
        public void Configure(EntityTypeBuilder<CitizenDetail> builder)
        {
            builder.Property(e => e.Id).HasMaxLength(128);

            builder.Property(e => e.CreateBy).IsRequired().HasMaxLength(128);
            builder.Property(e => e.LastModifiedBy).HasMaxLength(128);
            builder.Property(e => e.LastModified).HasColumnType("datetime");

            builder.Property(e => e.CardId).HasMaxLength(128);
            builder.Property(e => e.MotherId).HasMaxLength(128);
            builder.Property(e => e.FatherId).HasMaxLength(128);

            builder.Property(e => e.PrivateKey).IsRequired();

            builder.Property(e => e.DateOfBirth).HasColumnType("date");

            builder.Property(e => e.PhotoUrl).IsRequired().HasMaxLength(2048);

            builder.OwnsOne(e => e.Address);
            builder.OwnsOne(e => e.FullName);

            builder.HasOne(d => d.HealthInfo)
                .WithOne(i => i.Citizen)
                .HasForeignKey<HealthInfo>(e => e.CitizenId)
                .HasConstraintName("FK_HealthInfo_Citizen_CitizenId");
        }
    }
}
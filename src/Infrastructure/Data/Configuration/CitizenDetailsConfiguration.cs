using EGID.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EGID.Infrastructure.Data.Configuration
{
    public class CitizenDetailsConfiguration : IEntityTypeConfiguration<CitizenDetail>
    {
        public void Configure(EntityTypeBuilder<CitizenDetail> builder)
        {
            builder.Property(e => e.Id).HasMaxLength(128);

            builder.Property(e => e.CreateBy).HasMaxLength(128);
            builder.Property(e => e.LastModifiedBy).HasMaxLength(128);
            builder.Property(e => e.LastModified).HasColumnType("datetime");

            builder.Property(e => e.MotherId).HasMaxLength(128);
            builder.Property(e => e.FatherId).HasMaxLength(128);

            builder.Property(e => e.PrivateKey).IsRequired();
            builder.Property(e => e.PublicKey).IsRequired();

            builder.Property(e => e.DateOfBirth).HasColumnType("date");

            builder.Property(e => e.PhotoUrl).IsRequired().HasMaxLength(2048);

            builder.OwnsOne(e => e.Address, address =>
            {
                address.Property(a => a.Street).IsRequired().HasMaxLength(50);
                address.Property(a => a.City).IsRequired().HasMaxLength(50);
                address.Property(a => a.State).IsRequired().HasMaxLength(50);
                address.Property(a => a.PostalCode).IsRequired().HasMaxLength(50);
                address.Property(a => a.Country).IsRequired().HasMaxLength(50);
            });

            builder.OwnsOne(e => e.FullName, name =>
            {
                name.Property(n => n.FirstName).IsRequired().HasMaxLength(50);
                name.Property(n => n.SecondName).IsRequired().HasMaxLength(50);
                name.Property(n => n.ThirdName).IsRequired().HasMaxLength(50);
                name.Property(n => n.LastName).IsRequired().HasMaxLength(50);
            });

            builder.HasOne(d => d.HealthInfo)
                .WithOne(i => i.Citizen)
                .HasForeignKey<HealthInfo>(e => e.CitizenId)
                .HasConstraintName("FK_HealthInfo_Citizen_CitizenId");
        }
    }
}
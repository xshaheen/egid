using EGID.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EGID.Infrastructure.Data.Configuration
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.Property(e => e.Id).HasMaxLength(128);

            builder.Property(e => e.PhoneNumber).HasMaxLength(24);

            builder.Property(e => e.CitizenId).IsRequired().HasMaxLength(128);
            builder.Property(e => e.CardIssuer).IsRequired().HasMaxLength(128);

            builder.Property(e => e.Pin1Hash).IsRequired();
            builder.Property(e => e.Pin2Hash).IsRequired();

            builder.Property(e => e.Pin1Salt).IsRequired();
            builder.Property(e => e.Pin2Salt).IsRequired();

            builder.HasOne(e => e.Citizen)
                .WithOne(c => c.Card)
                .HasForeignKey<Card>(c => c.CitizenId);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EGID.Infrastructure.Auth.Configuration
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.Property(e => e.Id).HasMaxLength(128);

            builder.Property(e => e.PhoneNumber).HasMaxLength(24);

            builder.Property(e => e.CitizenId).IsRequired().HasMaxLength(128);
            builder.Property(e => e.CardIssuer).IsRequired().HasMaxLength(128);

            builder.Property(e => e.PublicKeyXml).IsRequired();
            builder.Property(e => e.PrivateKeyXml).IsRequired();

            builder.Property(e => e.Pin1).IsRequired().HasMaxLength(128);
            builder.Property(e => e.Pin2).IsRequired().HasMaxLength(128);
            builder.Property(e => e.Puk).IsRequired().HasMaxLength(128);

        }
    }
}

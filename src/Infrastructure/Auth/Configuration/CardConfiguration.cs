using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EGID.Infrastructure.Auth.Configuration
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.Property(e => e.Id).HasMaxLength(128);
            builder.Property(e => e.OwnerId).IsRequired().HasMaxLength(128);
            builder.Property(e => e.CardIssuer).IsRequired().HasMaxLength(128);

            builder.Property(e => e.Pin1).IsRequired().HasMaxLength(128);
            builder.Property(e => e.Pin2).IsRequired().HasMaxLength(128);
            builder.Property(e => e.Puk).IsRequired().HasMaxLength(128);

            builder.HasOne(e => e.Owner)
                .WithMany(e => e.Cards)
                .HasForeignKey(e => e.OwnerId)
                .HasConstraintName("FK_Cards_Owner_OwnerId");
        }
    }
}

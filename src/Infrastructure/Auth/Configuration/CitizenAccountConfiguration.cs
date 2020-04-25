using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EGID.Infrastructure.Auth.Configuration
{
    public class CitizenAccountConfiguration : IEntityTypeConfiguration<CitizenAccount>
    {
        public void Configure(EntityTypeBuilder<CitizenAccount> builder)
        {
            builder.Property(e => e.Id).HasMaxLength(128);

            builder.Property(e => e.PhoneNumber).HasMaxLength(24);

            builder.Property(e => e.CitizenId).IsRequired().HasMaxLength(128);
        }
    }
}

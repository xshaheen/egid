using EGID.Web.Entities;
using Microsoft.EntityFrameworkCore;

namespace EGID.Web.Data
{
    public class EgidDbContext : DbContext
    {
        public EgidDbContext(DbContextOptions<EgidDbContext> options) : base(options) {}

        public DbSet<HealthInfo> HealthInfo { get; set; }

        public DbSet<HealthRecord> HealthRecord { get; set; }
    }
}

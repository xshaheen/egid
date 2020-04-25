using System;

namespace EGID.Domain.Entities
{
    public class DeathCertificate : AuditableEntity
    {
        public string Id { get; set; }

        public string CitizenId { get; set; }
        public virtual CitizenDetails Citizen { get; set; }

        public string CauseOfDeath { get; set; }
        public DateTime DateOfDeath { get; set; }
    }
}

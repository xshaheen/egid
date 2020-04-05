using System;

namespace EGID.Domain.Entities
{
    public class DeathCertificate
    {
        public string Id { get; set; }

        public string CitizenId { get; set; }

        public string CauseOfDeath { get; set; }

        public DateTime Date { get; set; }

        public DateTime RecordDateTime { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace EGID.Domain.Entities
{
    public class HealthRecord : AuditableEntity
    {
        public string Id { get; set; }

        public string CitizenId { get; set; }

        public DateTime Date { get; set; }

        public string Medications { get; set; }

        public string Diagnosis { get; set; }

        public ICollection<string> Images { get; set; }
    }
}

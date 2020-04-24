using System;
using System.Collections.Generic;

namespace EGID.Domain.Entities
{
    public class ExitHospitalRecord : AuditableEntity
    {
        public string Id { get; set; }

        public string CitizenId { get; set; }

        public DateTime EnterDate { get; set; }

        public DateTime ExitDate { get; set; }

        public string Medications { get; set; }

        public string Diagnosis { get; set; }

        public ICollection<string> Images { get; set; }
    }
}

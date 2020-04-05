using System;
using System.Collections.Generic;

namespace EGID.Domain.Entities
{
    public class HealthRecord
    {
        public string Id { get; set; }

        public string CitizenId { get; set; }

        public string DoctorId { get; set; }

        public DateTime Date { get; set; }

        public string Medications { get; set; }

        public string Diagnosis { get; set; }

        public ICollection<string> Images { get; set; }
    }
}

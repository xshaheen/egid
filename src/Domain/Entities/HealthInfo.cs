using System.Collections.Generic;
using EGID.Domain.Enums;

namespace EGID.Domain.Entities
{
    public class HealthInfo
    {
        public HealthInfo() => HealthRecords = new HashSet<HealthRecord>();

        public string Id { get; set; }

        public BloodType BloodType { get; set; }

        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }

        public string CitizenId { get; set; }
        public virtual CitizenDetail Citizen { get; set; }

        public virtual ICollection<HealthRecord> HealthRecords { get; }
    }
}
using System.Collections.Generic;
using EGID.Core.Common;
using EGID.Domain.Enums;

namespace EGID.Domain.Entities
{
    public class HealthInfo : AuditableEntity
    {
        public string Id { get; set; }

        public string CitizenId { get; set; }

        public BloodType BloodType { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Phone3 { get; set; }

        public ICollection<HealthRecord> HealthRecords { get; set; }

        public ICollection<string> ExitHospitalRecords { get; set; }
    }
}

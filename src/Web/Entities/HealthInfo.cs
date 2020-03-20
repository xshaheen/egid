using System;
using System.Collections.Generic;
using EGID.Core.Enums;

namespace EGID.Web.Entities
{
    public class HealthInfo
    {
        public Guid Id { get; set; }

        public Guid CitizenId { get; set; }

        public BloodType BloodType { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Phone3 { get; set; }

        public ICollection<HealthRecord> HealthRecords { get; set; }
    }
}

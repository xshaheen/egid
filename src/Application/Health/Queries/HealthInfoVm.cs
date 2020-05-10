using System.Collections.Generic;
using EGID.Domain.Enums;
using EGID.Domain.ValueObjects;

namespace EGID.Application.Health.Queries
{
    public class HealthInfoVm
    {
        public string Id { get; set; }

        public FullName CitizenName { get; set; }
        public string CitizenPhoto { get; set; }

        public BloodType BloodType { get; set; }

        public string Notes { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }

        public List<HealthRecordVm> HealthRecords { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace EGID.Domain.Entities
{
    public class ExitHospitalRecord : AuditableEntity
    {
        public string Id { get; set; }

        public string HealthInfoId { get; set; }
        public HealthInfo HealthInfo { get; set; }

        public DateTime EnterDate { get; set; }
        public DateTime ExitDate { get; set; }

        public string Medications { get; set; }

        public string Diagnosis { get; set; }

        public ICollection<ExitHospitalRecordAttachment> Attachments { get; set; }
    }
}

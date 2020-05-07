using System;
using System.Collections.Generic;

namespace EGID.Application.Health.Queries
{
    public class HealthRecordVm
    {
        public string Medications { get; set; }

        public string Diagnosis { get; set; }

        public DateTime Create { get; set; }

        public string CreateBy { get; set; }

        public virtual ICollection<string> Attachments { get; set; }
    }
}
using System;

namespace EGID.Domain
{
    public class AuditableEntity
    {
        public DateTime Create { get; set; }

        public string CreateBy { get; set; }

        public DateTime? LastModified { get; set; }

        public string LastModifiedBy { get; set; }
    }
}

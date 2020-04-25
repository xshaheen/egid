using System;
using EGID.Domain.Enums;
using EGID.Domain.ValueObjects;

namespace EGID.Domain.Entities
{
    public class CitizenDetails : AuditableEntity
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string PrivateKey { get; set; }

        public string FatherId { get; set; }

        public string MotherId { get; set; }

        public FullName FullName { get; set; }

        public Address Address { get; set; }

        public Gender Gender { get; set; }

        public Religion Religion { get; set; }

        public SocialStatus SocialStatus { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PhotoUrl { get; set; }

        public virtual HealthInfo HealthInfo { get; set; }
        public virtual DeathCertificate DeathCertificate { get; set; }

        public override string ToString() => FullName.ToString();
    }
}

using System;
using EGID.Core.Enums;
using EGID.Core.ValueObjects;

namespace EGID.Web.Entities
{
    public class Citizen
    {
        public string Id { get; set; }

        public string PublicKey { get; set; }

        public FullName FullName { get; set; }

        public Address Address { get; set; }

        public Guid FatherId { get; set; }

        public Guid MotherId { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime? DateOfDeath { get; set; }

        public DateTime DateTime { get; set; }

        public string PhotoUrl { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public override string ToString() => FullName.ToString();
    }
}

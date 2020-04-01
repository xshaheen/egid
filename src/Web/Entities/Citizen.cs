using System;
using System.ComponentModel.DataAnnotations.Schema;
using EGID.Core.Common;
using EGID.Core.Enums;
using EGID.Core.ValueObjects;

namespace EGID.Web.Entities
{
    public class Citizen : AuditableEntity
    {
        public Guid Id { get; set; }

        [InverseProperty(nameof(Entities.Card.OwnerId))]
        public Card Card { get; set; }

        public string PrivateKey { get; set; }

        public FullName FullName { get; set; }

        public Address Address { get; set; }

        public Guid FatherId { get; set; }

        public Guid MotherId { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }
 
        public DateTime? DateOfDeath { get; set; }

        public string PhotoUrl { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public override string ToString() => FullName.ToString();
    }
}

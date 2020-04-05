using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EGID.Data
{
    public class Card : IdentityUser
    {
        [ForeignKey(nameof(Card))] public Guid OwnerId { get; set; }

        public string PrivateKeyXml { get; set; }

        public string PublicKeyXml { get; set; }

        public string Pin1 { get; set; }

        public string Pin2 { get; set; }

        public string Puk { get; set; }

        public string CardIssuer { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime TerminationDate { get; set; }

        public bool Active { get; set; }

        public void Activate() => Active = true;

        public void Inactivate() => Active = false;
    }
}

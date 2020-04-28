using System;
using Microsoft.AspNetCore.Identity;

namespace EGID.Infrastructure.Auth
{
    public class Card : IdentityUser
    {
        public string CitizenId { get; set; }

        public string PrivateKeyXml { get; set; }
        public string PublicKeyXml { get; set; }

        public string Pin1 { get; set; }
        public string Pin2 { get; set; }
        public string Puk { get; set; }

        public string CardIssuer { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime TerminationDate { get; set; }

        public bool Active { get; set; }

        #region Helpers

        public void Activate() => Active = true;

        public void Inactivate() => Active = false;

        #endregion
    }
}

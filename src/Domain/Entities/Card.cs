using System;
using Microsoft.AspNetCore.Identity;

namespace EGID.Domain.Entities
{
    public class Card : IdentityUser
    {
        public string CitizenId { get; set; }
        public CitizenDetail Citizen { get; set; }

        public string PrivateKeyXml { get; set; }
        public string PublicKeyXml { get; set; }

        public string Pin1Hash { get; set; }
        public string Pin1Salt { get; set; }
        public string Pin2Hash { get; set; }
        public string Pin2Salt { get; set; }

        public string CardIssuer { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime TerminationDate { get; set; }

        public bool Active { get; set; }

        #region Helpers

        public void Activate()
        {
            Active = true;
        }

        public void Inactivate()
        {
            Active = false;
        }

        #endregion
    }
}
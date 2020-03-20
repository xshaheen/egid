using System;

namespace EGID.Web.Entities
{
    public class Card
    {
        public Guid Id { get; set; }

        public string PublicId { get; set; }

        public string Pin1 { get; set; }

        public string Pin2 { get; set; }

        public string Puk { get; set; }

        public string CardIssuer { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime TerminationDate { get; set; }
    }
}

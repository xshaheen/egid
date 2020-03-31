using System;

namespace EGID.Web.Data.Repository.Cards.Dto
{
    public class CreateCardDto
    {
        public Guid OwnerId { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Puk { get; set; }

        public string Pin1 { get; set; }

        public string Pin2 { get; set; }
    }
}

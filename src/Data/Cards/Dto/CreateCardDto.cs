using System;

namespace EGID.Data.Cards.Dto
{
    public class CreateCardDto
    {
        public Guid OwnerId { get; set; }

        public string Puk { get; set; }

        public string Pin1 { get; set; }

        public string Pin2 { get; set; }
    }
}

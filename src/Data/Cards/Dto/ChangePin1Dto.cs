using System;

namespace EGID.Data.Cards.Dto
{
    public class ChangePin1Dto
    {
        public Guid CardId { get; set; }

        public string Puk { get; set; }

        public string NewPin1 { get; set; }
    }
}

using System;

namespace EGID.Data.Cards.Dto
{
    public class ChangePin2Dto
    {
        public Guid CardId { get; set; }

        public string Puk { get; set; }

        public string NewPin2 { get; set; }
    }
}

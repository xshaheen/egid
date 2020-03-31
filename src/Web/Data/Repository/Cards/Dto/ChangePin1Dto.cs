using System;

namespace EGID.Web.Data.Repository.Cards.Dto
{
    public class ChangePin1Dto
    {
        public Guid CardId { get; set; }

        public string Puk { get; set; }

        public string NewPin1 { get; set; }
    }
}

using System;

namespace EGID.Web.Data.Repository.Cards.Dto
{
    public class ChangePin2Dto
    {
        public Guid CardId { get; set; }

        public string Puk { get; set; }

        public string NewPin2 { get; set; }
    }
}

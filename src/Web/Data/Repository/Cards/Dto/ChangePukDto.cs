using System;

namespace EGID.Web.Data.Repository.Cards.Dto
{
    public class ChangePukDto
    {
        public Guid CardId { get; set; }

        public string CurrentPuk { get; set; }

        public string NewPuk { get; set; }
    }
}

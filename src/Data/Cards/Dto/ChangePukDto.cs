using System;

namespace EGID.Data.Cards.Dto
{
    public class ChangePukDto
    {
        public Guid CardId { get; set; }

        public string CurrentPuk { get; set; }

        public string NewPuk { get; set; }
    }
}

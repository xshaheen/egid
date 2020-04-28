using System;

namespace EGID.Infrastructure.Auth.Models
{
    public class CreateCardModel
    {
        public Guid OwnerId { get; set; }

        public string Puk { get; set; }

        public string Pin1 { get; set; }

        public string Pin2 { get; set; }
    }
}

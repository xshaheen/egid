using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace EGID.Infrastructure.Auth
{
    public class CitizenAccount : IdentityUser
    {
        public string CitizenId { get; set; }
        
        public virtual ICollection<Card> Cards { get; set; }
    }
}

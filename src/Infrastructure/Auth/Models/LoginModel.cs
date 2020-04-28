using System.ComponentModel.DataAnnotations;

namespace EGID.Infrastructure.Auth.Models
{
    public class LoginModel
    {
        [Required] public string PrivateKey { get; set; }

        [Required] public string Pin1 { get; set; }
    }
}

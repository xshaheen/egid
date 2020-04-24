using System.ComponentModel.DataAnnotations;

namespace EGID.Data.Cards.Dto
{
    public class SignInDto
    {
        [Required] public string PrivateKey { get; set; }

        [Required] public string Pin1 { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace EGID.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignController : ControllerBase
    {
        // citizen send hash code and his card private key to sign a hash


        // citizen send signature and public key of other citizen and will verify
        // the signature and send basic info about public key owner
    }
}

using Microsoft.AspNetCore.Mvc;

namespace EGID.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        // sign in | should send card private key and password
        // send back a jwt token to use for a 30min
    }
}

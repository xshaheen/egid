using Microsoft.AspNetCore.Mvc;

namespace EGID.Web.Controllers
{
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public string Get() => "Hello EGID";
    }
}

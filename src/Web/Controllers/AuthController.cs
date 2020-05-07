using System.Threading.Tasks;
using EGID.Application.Cards.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EGID.Web.Controllers
{
    [AllowAnonymous]
    public class AuthController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> SignIn([FromBody] LoginCommand command)
        {
            var token = await Mediator.Send(command);

            return Ok(token);
        }
    }
}
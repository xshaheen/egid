using System.Threading.Tasks;
using EGID.Application.Health.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EGID.Web.Controllers
{
    public class HealthInfoController : BaseController
    {
        // get health information records
        [HttpGet("/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string citizenId)
        {
            await Mediator.Send(new GetHealthInfoQuery());

            return Ok();
        }

        // add a record

        // add a hospital discharge
    }
}
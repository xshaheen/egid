using System.Threading.Tasks;
using EGID.Application.Health.Commands;
using EGID.Application.Health.Queries;
using EGID.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EGID.Web.Controllers
{
    [Authorize]
    public class HealthInfoControllerBase : ApiControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string citizenId)
        {
            await Mediator.Send(new GetHealthInfoQuery());

            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Post([FromBody]AddHealthRecordCommand command)
        {
            command.Attachments = Request.Files();

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> EmergencyPhones([FromBody]UpdateEmergencyPhonesCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

    }
}
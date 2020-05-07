using System.Threading.Tasks;
using EGID.Application.Cards.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EGID.Web.Controllers
{
    public class CardControllerBase : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Post([FromBody] CreateCardCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Puk([FromBody] ChangePukCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Pin1([FromBody] ChangePin1Command command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Pin2([FromBody] ChangePin2Command command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
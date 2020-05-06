using System.Threading.Tasks;
using EGID.Application.CitizenDetails.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EGID.Web.Controllers
{
    public class CitizensController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Post([FromBody] CreateCitizenCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
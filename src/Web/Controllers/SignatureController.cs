using System.Threading.Tasks;
using EGID.Application.Cards.Commands;
using EGID.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace EGID.Web.Controllers
{
    [Authorize]
    public class SignatureController : ApiControllerBase
    {
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(FileContentResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<FileContentResult> Sign([FromBody] SignHashCommand command)
        {
            var signature = await Mediator.Send(command);

            return File(signature.Bytes, signature.ContentType, signature.Name);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VerifySignatureResult>> Verify(
            [FromBody] VerifySignatureCommand command)
        {
            var (valid, fullName, photo) = await Mediator.Send(command);

            return Ok(new VerifySignatureResult{FullName = fullName, Valid = valid, Photo = photo});
        }
    }

    public class VerifySignatureResult
    {
        public bool Valid { get; set; }

        public FullName FullName { get; set; }

        public string Photo { get; set; }
    }
}
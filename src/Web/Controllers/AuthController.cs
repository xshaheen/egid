using System.Threading.Tasks;
using EGID.Data.Cards;
using EGID.Data.Cards.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EGID.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly ICardService _service;

        public AuthController(ICardService service) => _service = service;

        [HttpPost]
        public async Task<ActionResult> SignIn([FromBody] SignInDto signInDto)
        {
            if (ModelState.IsValid)
                return UnprocessableEntity(ModelState.Values);

            var result = await _service.SignInAsync(signInDto);
            if (result.Failed)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }
    }
}

using System.Threading.Tasks;
using EGID.Web.Data.Repository.Cards;
using EGID.Web.Data.Repository.Cards.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EGID.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly ICardRepo _repo;

        public AuthController(ICardRepo repo) => _repo = repo;

        [HttpPost]
        public async Task<ActionResult> SignIn([FromBody] SignInDto signInDto)
        {
            if (ModelState.IsValid)
                return UnprocessableEntity(ModelState.Values);

            var result = await _repo.SignInAsync(signInDto);
            if (result.Failed)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }
    }
}

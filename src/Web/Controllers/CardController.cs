using Microsoft.AspNetCore.Mvc;

namespace EGID.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }

        // [HttpPost]
        // public async Task<ActionResult> Post([FromBody] CreateCardModel model)
        // {
        //     var (result, id) = await _cardManagerService.RegisterAsync(model);
        //
        //     if (result.Succeeded) return Ok(id);
        //
        //     return BadRequest(new {errors = result.Errors});
        // }
    }
}
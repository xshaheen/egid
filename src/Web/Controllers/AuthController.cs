using System;
using EGID.Application;
using EGID.Infrastructure.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EGID.Web.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IKeyGeneratorService _keyGenerator;
        private readonly IConfiguration _configuration;

        public AuthController(IKeyGeneratorService keyGenerator, IConfiguration configuration)
        {
            _keyGenerator = keyGenerator;
            _configuration = configuration;
        }

        [HttpPost]
        public ActionResult SignIn()
        {
            // validate
            // if (ModelState.IsValid)
            //     return UnprocessableEntity(ModelState.Values);

            var card = new Card
            {
                Id = Guid.NewGuid().ToString(),
                PublicKeyXml = _keyGenerator.PublicKeyXml,
                PrivateKeyXml = _keyGenerator.PrivateKeyXml,
                Puk = "123456",
                Pin1 = "123456",
                Pin2 = "123456",
            };

            // process

            // if (result.Failed)
            //     return BadRequest(result.Errors);

            return Ok(/*GenerateJwtToken(card.Id)*/);
        }

        [HttpPost]
        public ActionResult ChangePuk()
        {
            return Ok();
        }

        [HttpPost]
        public ActionResult ChangePin1()
        {
            return Ok();
        }

        [HttpPost]
        public ActionResult ChangePin2()
        {
            return Ok();
        }
    }
}
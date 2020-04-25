using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EGID.Data.Cards;
using EGID.Infrastructure.Auth;
using EGID.Infrastructure.KeysGenerator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EGID.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
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

            return Ok(GenerateJwtToken(card.Id));
        }

        private string GenerateJwtToken(string cardId)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, cardId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            // Expires
            var minutes = Convert.ToDouble(_configuration["JwtExpireMinutes"]);
            var expires = DateTime.Now.AddMinutes(minutes);

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: cred
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet]
        [Authorize]
        public string Secret()
        {
            return "This is secret";
        }
    }
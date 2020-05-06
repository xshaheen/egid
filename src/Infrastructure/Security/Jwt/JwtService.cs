using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace EGID.Infrastructure.Security.Jwt
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSettings _settings;
        private readonly SigningCredentials _signingCredentials;

        public JwtTokenService(JwtSettings settings)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            _signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
            _settings = settings;
        }

        public Dictionary<string, object> Decode(string token)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(token).Payload;
        }

        public string Generate(IList<Claim> claims)
        {
            claims ??= new List<Claim>();

            var jwtSecurityToken = new JwtSecurityToken
            (
                _settings.Issuer,
                _settings.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.Add(_settings.Expires),
                _signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
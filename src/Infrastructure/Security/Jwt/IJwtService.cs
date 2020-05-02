using System.Collections.Generic;
using System.Security.Claims;

namespace EGID.Infrastructure.Security.Jwt
{
    public interface IJwtTokenService
    {
        Dictionary<string, object> Decode(string token);

        string Generate(IList<Claim> claims);
    }
}

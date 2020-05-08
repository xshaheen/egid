using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EGID.Application.Common
{
    public static class ClaimExtensions
    {
        public static IList<Claim> AddNameIdentifier(this IList<Claim> claims, string id)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, id));

            return claims;
        }

        public static IList<Claim> AddJti(this IList<Claim> claims)
        {
            claims.Add(new Claim("jti", Guid.NewGuid().ToString()));

            return claims;
        }

        public static IList<Claim> AddRoles(this IList<Claim> claims, string[] roles)
        {
            roles.ToList().ForEach(role => claims.Add(new Claim("role", role)));

            return claims;
        }

        public static IList<Claim> AddSub(this IList<Claim> claims, string sub)
        {
            claims.Add(new Claim("sub", sub));

            return claims;
        }
    }
}
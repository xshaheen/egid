using System;

namespace EGID.Infrastructure.Security.Jwt
{
    public class JwtSettings
    {
        public JwtSettings
        (
            string key,
            TimeSpan expires
        )
        {
            Key = key;
            Expires = expires;
        }

        public JwtSettings
        (
            string key,
            TimeSpan expires,
            string audience,
            string issuer
        )
            : this(key, expires)
        {
            Audience = audience;
            Issuer = issuer;
        }

        public string Audience { get; }

        public TimeSpan Expires { get; }

        public string Issuer { get; }

        public string Key { get; }
    }
}
using Microsoft.Extensions.Configuration;

namespace EGID.Infrastructure.Auth.Services
{
    public class PrivateKeyOptions
    {
        public PrivateKeyOptions(IConfiguration configuration)
        {
            configuration.GetSection("PrivateKey").Bind(this);
        }

        public string PrivateKeyEncryptionKey { get; set; }
        public string PrivateKeyEncryptionIv { get; set; }
    }
}
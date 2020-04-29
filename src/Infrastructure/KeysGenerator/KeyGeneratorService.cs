using System.Security.Cryptography;
using EGID.Application;

namespace EGID.Infrastructure.KeysGenerator
{
    /// <summary>
    ///     Generate RSA Keys as XML string.
    /// </summary>
    public class KeyGeneratorService : IKeyGeneratorService
    {
        public KeyGeneratorService()
        {
            using var rsa = new RSACryptoServiceProvider(4096) { PersistKeyInCsp = false };

            PrivateKeyXml = rsa.ToXmlString(true);
            PublicKeyXml = rsa.ToXmlString(false);
        }

        public string PrivateKeyXml { get; }

        public string PublicKeyXml { get; }
    }
}

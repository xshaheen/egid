using System.Security.Cryptography;
using EGID.Application;

namespace EGID.Infrastructure.Security
{
    /// <summary>
    ///     Generate RSA Keys as XML string.
    /// </summary>
    public class KeysGeneratorService : IKeysGeneratorService
    {
        public KeysGeneratorService()
        {
            using var rsa = new RSACryptoServiceProvider(4096) { PersistKeyInCsp = false };

            PrivateKeyXml = rsa.ToXmlString(true);
            PublicKeyXml = rsa.ToXmlString(false);
        }

        public string PrivateKeyXml { get; }

        public string PublicKeyXml { get; }
    }
}

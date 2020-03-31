using System.Security.Cryptography;

namespace EGID.Web.Infra.KeysGeneratorService
{
    /// <summary>
    ///     Generate RSA Keys as XML string.
    /// </summary>
    public class KeyGenerator : IKeyGenerator
    {
        public KeyGenerator()
        {
            using var rsa = new RSACryptoServiceProvider(4096) { PersistKeyInCsp = false };

            PrivateKeyXml = rsa.ToXmlString(true);
            PublicKeyXml = rsa.ToXmlString(false);
        }

        public string PrivateKeyXml { get; }

        public string PublicKeyXml { get; }
    }
}

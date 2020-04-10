using System.Security.Cryptography;

namespace EGID.Infrastructure.DigitalSignature
{
    public class DigitalSignatureService : IDigitalSignatureService
    {
        public byte[] SignHash(byte[] sha512Hash, string privateKeyXml)
        {
            var rsa = new RSACryptoServiceProvider(4096) { PersistKeyInCsp = false };
            // import the private key used for signing the message
            rsa.FromXmlString(privateKeyXml);

            // Sign the hash, using SHA512 as the hashing algorithm
            return rsa.SignHash(sha512Hash, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);
        }

        public bool VerifySignature(
            byte[] dataBytes,
            byte[] signatureBytes,
            string publicKeyXml)
        {
            // instantiate RSA to verify
            using var rsa = RSA.Create();

            rsa.FromXmlString(publicKeyXml);

            // Verify signature, using SHA512 as the hashing algorithm
            return rsa.VerifyData(
                dataBytes,
                signatureBytes,
                HashAlgorithmName.SHA512,
                RSASignaturePadding.Pkcs1
            );
        }
    }
}

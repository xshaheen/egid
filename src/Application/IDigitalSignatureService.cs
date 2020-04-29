using System.Security.Cryptography;

namespace EGID.Application
{
    public interface IDigitalSignatureService
    {
        /// <summary>
        ///     Sign SHA-512 hash with RSA algorithm using the provider privateKey as
        ///     XML string.
        /// </summary>
        /// <param name="sha512Hash">The hash value of the data to be signed.</param>
        /// <param name="privateKeyXml">Private key to use</param>
        /// <returns>The RSA signature for the specified hash value.</returns>
        /// <exception cref="CryptographicException">
        ///     The cryptographic service provider (CSP) cannot be acquired.
        ///     -or-
        ///     The format of the xml private key string parameter is not valid.
        /// </exception>
        byte[] SignHash(byte[] sha512Hash, string privateKeyXml);

        /// <summary>
        ///     Verify a RSA/SHA512 digital Signature using the provided public key.
        /// </summary>
        /// <param name="dataBytes">The signed data.</param>
        /// <param name="signatureBytes">The signature data to be verified.</param>
        /// <param name="publicKeyXml">Public key to use</param>
        /// <returns>Verifies that a digital signature is valid.</returns>
        /// <exception cref="CryptographicException">
        ///     The cryptographic service provider (CSP) cannot be acquired.
        ///     -or-
        ///     The format of the xml private key string parameter is not valid.
        /// </exception>
        bool VerifySignature(
            byte[] dataBytes,
            byte[] signatureBytes,
            string publicKeyXml
        );
    }
}

using System.Security.Cryptography;

namespace EGID.Application.Common.Interfaces
{
    public interface IDigitalSignatureService
    {
        /// <summary>
        ///     Sign a base64 SHA-512 hash with RSA algorithm using the provider privateKey as
        ///     XML string.
        /// </summary>
        /// <param name="base64Hash">Base64 hash value of the data to be signed.</param>
        /// <param name="privateKeyXml">Private key to use</param>
        /// <returns>A base64 RSA signature for the specified hash value.</returns>
        /// <exception cref="CryptographicException">
        ///     The cryptographic service provider (CSP) cannot be acquired.
        ///     -or-
        ///     The format of the xml private key string parameter is not valid.
        /// </exception>
        string SignHash(string base64Hash, string privateKeyXml);

        /// <summary>
        ///     Verify a RSA/SHA512 digital Signature using the provided public key.
        /// </summary>
        /// <param name="dataHash">Base64 SHA512 hash of the data.</param>
        /// <param name="signature">The signature SHA512 hash to be verified.</param>
        /// <param name="publicKeyXml">Public key to use</param>
        /// <returns>Verifies that a digital signature is valid.</returns>
        /// <exception cref="CryptographicException">
        ///     The cryptographic service provider (CSP) cannot be acquired.
        ///     -or-
        ///     The format of the xml private key string parameter is not valid.
        /// </exception>
        bool VerifySignature(string dataHash, string signature, string publicKeyXml);
    }
}
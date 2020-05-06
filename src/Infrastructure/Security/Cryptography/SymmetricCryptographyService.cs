using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EGID.Application.Common.Interfaces;

namespace EGID.Infrastructure.Security.Cryptography
{
    public class SymmetricCryptographyService : ISymmetricCryptographyService
    {
        private readonly string _key;

        public SymmetricCryptographyService(string key) => _key = key;

        public async Task<string> DecryptAsync(string cypherText, string salt)
        {
            using var algorithm = Algorithm(salt);

            byte[] plainBytes = await TransformAsync(Convert.FromBase64String(cypherText), algorithm.CreateDecryptor());

            return Encoding.Unicode.GetString(plainBytes);
        }

        public async Task<string> DecryptAsync(string cypherText)
        {
            return await DecryptAsync(cypherText, string.Empty);
        }

        public async Task<string> EncryptAsync(string plainText, string salt)
        {
            using var algorithm = Algorithm(salt);

            byte[] cypherBytes =
                await TransformAsync(Encoding.Unicode.GetBytes(plainText), algorithm.CreateEncryptor());

            return Convert.ToBase64String(cypherBytes);
        }

        public async Task<string> EncryptAsync(string plainText)
        {
            return await EncryptAsync(plainText, string.Empty);
        }

        /// <summary>
        ///     Initialize and return the symmetric algorithm.
        /// </summary>
        private SymmetricAlgorithm Algorithm(string salt)
        {
            if (string.IsNullOrWhiteSpace(salt)) salt = _key;

            using var key = new Rfc2898DeriveBytes(_key, Encoding.ASCII.GetBytes(salt));

            var algorithm = new RijndaelManaged();

            algorithm.Key = key.GetBytes(algorithm.KeySize / 8);
            algorithm.IV = key.GetBytes(algorithm.BlockSize / 8);

            return algorithm;
        }

        /// <summary>
        ///     Do cryptographic transformation (encrypt/decrypt) using <paramref name="cryptoTransform"/>.
        /// </summary>
        private static async Task<byte[]> TransformAsync(byte[] bytes, ICryptoTransform cryptoTransform)
        {
            using (cryptoTransform)
            {
                // Create temporary MemoryStream to store the results
                await using var memoryStream = new MemoryStream();

                await using var cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);

                await cryptoStream.WriteAsync(bytes, 0, bytes.Length);

                cryptoStream.Close();

                return memoryStream.ToArray();
            }
        }
    }
}
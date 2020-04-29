using System;
using System.Threading.Tasks;

namespace EGID.Infrastructure.Crypto
{
    public static class StringExtensions
    {
        public static async Task<string> ToAesCypher(this string msg, string iv, string key)
        {
            var aes = new AesCryptoService();

            var encryptedBytes = await aes.EncryptAsync(
                Convert.FromBase64String(key),
                Convert.FromBase64String(iv),
                msg
            );

            return Convert.ToBase64String(encryptedBytes);
        }

        public static async Task<string> ToAesPlain(this string msg, string iv, string key)
        {
            var aes = new AesCryptoService();

            var encryptedBytes = await aes.DecryptAsync(
                Convert.FromBase64String(key),
                Convert.FromBase64String(iv),
                msg
            );

            return Convert.ToBase64String(encryptedBytes);
        }
    }
}
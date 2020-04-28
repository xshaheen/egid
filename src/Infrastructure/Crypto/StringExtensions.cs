using System;
using System.Text;
using System.Threading.Tasks;

namespace EGID.Infrastructure.Crypto
{
    public static class StringExtensions
    {
        public static async Task<string> ToAesCypher(this string msg, string iv, string key)
        {
            var aes = new AesCryptoService();

            var encryptedBytes = await aes.EncryptAsync(
                Encoding.UTF8.GetBytes(key),
                Encoding.UTF8.GetBytes(iv),
                msg
            );

            return Convert.ToBase64String(encryptedBytes);
        }

        public static async Task<string> ToAesPlain(this string msg, string iv, string key)
        {
            var aes = new AesCryptoService();

            var encryptedBytes = await aes.DecryptAsync(
                Encoding.UTF8.GetBytes(key),
                Encoding.UTF8.GetBytes(iv),
                msg
            );

            return Convert.ToBase64String(encryptedBytes);
        }
    }
}
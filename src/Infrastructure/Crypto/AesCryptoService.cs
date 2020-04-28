using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EGID.Infrastructure.Crypto
{
    public class AesCryptoService : IAesCryptoService
    {
        public async Task<byte[]> EncryptAsync(byte[] key, byte[] iv, string msg)
        {
            using var aes = new AesManaged
            {
                Key = key,
                IV = iv,
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC
            };

            // create an encryptor to perform stream transformation
            var encryptor = aes.CreateEncryptor();

            // create temporary MemoryStream to store the results
            await using var ms = new MemoryStream();

            var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);

            // create a StreamWriter to write UTF8 string
            var sw = new StreamWriter(cs);
            await sw.WriteAsync(msg);

            // flush data
            await sw.FlushAsync();
            cs.FlushFinalBlock();

            return ms.ToArray();
        }

        public async Task<byte[]> EncryptAsync(byte[] key, byte[] iv, byte[] msg)
        {
            using var aes = new AesManaged
            {
                Key = key,
                IV = iv,
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC
            };

            // create an encryptor to perform stream transformation
            var encryptor = aes.CreateEncryptor();

            // create temporary MemoryStream to store the results
            await using var ms = new MemoryStream();

            var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);

            await cs.WriteAsync(msg, 0, msg.Length);

            cs.FlushFinalBlock();

            return ms.ToArray();
        }

        public async Task<byte[]> DecryptAsync(byte[] key, byte[] iv, string msg)
        {
            using var aes = new AesManaged
            {
                Key = key,
                IV = iv,
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC
            };

            // create an decryptor to perform stream transformation
            var decryptor = aes.CreateDecryptor();

            // create temporary MemoryStream to store the results
            await using var ms = new MemoryStream();

            await using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
            {
                await cs.WriteAsync(Encoding.UTF8.GetBytes(msg), 0, msg.Length);
            }

            return ms.ToArray();
        }

        public async Task<byte[]> DecryptAsync(byte[] key, byte[] iv, byte[] msg)
        {
            using var aes = new AesManaged
            {
                Key = key,
                IV = iv,
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC
            };

            // create an decryptor to perform stream transformation
            var decryptor = aes.CreateDecryptor();

            // create temporary MemoryStream to store the results
            await using var ms = new MemoryStream();

            await using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
            {
                await cs.WriteAsync(msg, 0, msg.Length);
            }

            return ms.ToArray();
        }
    }
}

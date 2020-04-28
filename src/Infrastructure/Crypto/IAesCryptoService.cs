using System.Threading.Tasks;

namespace EGID.Infrastructure.Crypto
{
    public interface IAesCryptoService
    {
        /// <summary>
        ///     Encrypt a UTF8 string <paramref name="msg" /> using <paramref name="key" /> and initial vector
        ///     <paramref name="iv" />
        /// </summary>
        Task<byte[]> EncryptAsync(byte[] key, byte[] iv, string msg);

        /// <summary>
        ///     Encrypt a byte array <paramref name="msg" /> using <paramref name="key" /> and initial vector
        ///     <paramref name="iv" />
        /// </summary>
        Task<byte[]> EncryptAsync(byte[] key, byte[] iv, byte[] msg);

        /// <summary>
        ///     Decrypt a byte array <paramref name="msg" /> using <paramref name="key" /> and initial vector
        ///     <paramref name="iv" />
        /// </summary>
        Task<byte[]> DecryptAsync(byte[] key, byte[] iv, byte[] msg);
    }
}

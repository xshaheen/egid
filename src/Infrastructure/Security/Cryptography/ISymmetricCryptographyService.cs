using System.Threading.Tasks;

namespace EGID.Infrastructure.Security.Cryptography
{
    public interface ISymmetricCryptographyService
    {
        Task<string> DecryptAsync(string cypherText);

        Task<string>  DecryptAsync(string cypherText, string salt);

        Task<string>  EncryptAsync(string plainText);

        Task<string>  EncryptAsync(string plainText, string salt);
    }
}

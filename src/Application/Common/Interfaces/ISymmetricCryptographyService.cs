using System.Threading.Tasks;

namespace EGID.Application.Common.Interfaces
{
    public interface ISymmetricCryptographyService
    {
        Task<string> DecryptAsync(string cypherText);

        Task<string> DecryptAsync(string cypherText, string salt);

        Task<string> EncryptAsync(string plainText);

        Task<string> EncryptAsync(string plainText, string salt);
    }
}
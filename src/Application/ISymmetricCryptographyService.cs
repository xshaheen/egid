 using System.Threading.Tasks;

 namespace EGID.Application
{
    public interface ISymmetricCryptographyService
    {
        Task<string> DecryptAsync(string cypherText);

        Task<string>  DecryptAsync(string cypherText, string salt);

        Task<string>  EncryptAsync(string plainText);

        Task<string>  EncryptAsync(string plainText, string salt);
    }
}

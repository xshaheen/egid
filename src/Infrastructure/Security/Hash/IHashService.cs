namespace EGID.Infrastructure.Security.Hash
{
    public interface IHashService
    {
        string Create(string value, string salt);
    }
}
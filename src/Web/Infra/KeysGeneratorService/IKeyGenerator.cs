using System.Threading.Tasks;

namespace EGID.Web.Infra.KeysGeneratorService
{
    public interface IKeyGenerator
    {
        Task<(byte[] PublicKey, byte[] PrivateKey)> Generate();
    }
}

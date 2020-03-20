using System.Threading.Tasks;

namespace EGID.Web.Infra.KeysGenerator
{
    public interface IKeyGenerator
    {
        Task<(byte[] PublicKey, byte[] PrivateKey)> Generate();
    }
}

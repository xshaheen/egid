using System.Threading.Tasks;

namespace EGID.Infra.KeysGenerator
{
    public interface IKeyGenerator
    {
        Task<(byte[] PublicKey, byte[] PrivateKey)> Generate();
    }
}

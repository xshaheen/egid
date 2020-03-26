using System.Threading.Tasks;

namespace EGID.Web.Infra.SignDocService
{
    public interface ISignDoc
    {
        Task SignDocAsync();

        Task VerifySignature();
    }
}

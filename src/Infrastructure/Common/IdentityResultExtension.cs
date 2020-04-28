using System.Linq;
using EGID.Common.Models.Result;
using Microsoft.AspNetCore.Identity;

namespace EGID.Infrastructure.Common
{
    public static class IdentityResultExtension
    {
        public static Result ToResult(this IdentityResult identityResult)
        {
            return identityResult.Succeeded
                ? Result.Success()
                : Result.Failure(identityResult.Errors.Select(i => i.Description));
        }
    }
}

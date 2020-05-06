using System.Security.Claims;
using EGID.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EGID.Web.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            IsAuthenticated = UserId != null;
        }

        public string UserId { get; }
        public bool IsAuthenticated { get; }
    }
}
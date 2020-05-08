using System.Security.Claims;
using EGID.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EGID.Web.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            CitizenId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            CardId = httpContextAccessor.HttpContext?.User?.FindFirstValue("sub");
            IsAuthenticated = CardId != null;
        }

        public string CitizenId { get; }

        public string CardId { get; }

        public bool IsAuthenticated { get; }
    }
}
using EGID.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EGID.Web.Services
{
    // Used for testing
    public class FakeCurrentUserService : ICurrentUserService
    {
        public FakeCurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            CitizenId = "1af4008c-312f-4eda-9c7f-0f892d934a81";
            CardId = "b9ced42d-d5d5-4b34-98a8-82754bac72d2";
            IsAuthenticated = true;
        }

        public string CitizenId { get; }

        public string CardId { get; }

        public bool IsAuthenticated { get; }
    }
}
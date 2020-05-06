using MediatR;

namespace EGID.Application.Health.Queries
{
    public class GetHealthInfoQuery : IRequest
    {
        public string CardId { get; set; }
    }
}
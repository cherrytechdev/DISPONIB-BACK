using ESApplication.AggregateModels;
using MediatR;

namespace ESApplication.Queries.GetTokenDetails
{
    public class GetTokenDetailsQuery : IRequest<TokenDto>
    {
        public String token { get; set; }
    }
}

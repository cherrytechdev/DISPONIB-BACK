using ESApplication.AggregateModels;
using MediatR;

namespace ESApplication.Queries.GetCommonDetails
{
    public class GetCommonDetailsQuery : IRequest<List<CommonDto>>
    {
        public string userid { get; set; }
        public int type { get; set; }
    }
}

using ESApplication.AggregateModels;
using MediatR;

namespace ESApplication.Queries.GetBusiness
{
    public class GetBusinessDetailsQuery : IRequest<List<BusinessDto>>
    {
        public string userid { get; set; }
        public string id { get; set; }
    }
}

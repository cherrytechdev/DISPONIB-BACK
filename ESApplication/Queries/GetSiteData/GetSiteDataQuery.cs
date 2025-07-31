using ESApplication.AggregateModels;
using MediatR;

namespace ESApplication.Queries.GetSiteDataDetails
{
    public class GetSiteDataDetailsQuery : IRequest<List<SiteDataDto>>
    {
        public string userid { get; set; }
    }
}

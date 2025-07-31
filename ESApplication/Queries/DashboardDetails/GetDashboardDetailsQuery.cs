using ESApplication.AggregateModels;
using MediatR;

namespace ESApplication.Queries.GetDashboard
{
    public class GetDashboardDetailsQuery : IRequest<List<DashboardDto>>
    {
        public string userid { get; set; }
        public string businessid { get; set; }
    }
}

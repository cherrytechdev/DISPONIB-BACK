using ESApplication.AggregateModels;
using MediatR;

namespace ESApplication.Queries.GetBusiness
{
    public class GetReportsDetailsQuery : IRequest<List<ReportsDto>>
    {
        public string code1 { get; set; }
        public string code2 { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; } 
    }
} 
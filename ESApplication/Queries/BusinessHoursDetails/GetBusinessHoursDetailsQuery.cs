using ESApplication.AggregateModels;
using MediatR;

namespace ESApplication.Queries.GetBusinessHours
{
    public class GetBusinessHoursDetailsQuery : IRequest<List<BusinessHoursDto>>
    {
        public string id { get; set; }
    }
}

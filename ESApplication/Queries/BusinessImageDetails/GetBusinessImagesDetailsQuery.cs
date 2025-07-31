using ESApplication.AggregateModels;
using MediatR;

namespace ESApplication.Queries.GetBusinessImages
{
    public class GetBusinessImagesDetailsQuery : IRequest<List<BusinessImagesDto>>
    {
        public string id { get; set; }
    }
}

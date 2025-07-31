using ESApplication.AggregateModels; 
using MediatR;  
namespace ESApplication.Queries.GetMasterSelectDetails
{
    public class GetMasterSelectDetailsQuery : IRequest<List<MasterSelectDetailsDto>>
    {
        public int type { get; set; }
        public string userid { get; set; }

        public string val1 { get; set; }
    }
}

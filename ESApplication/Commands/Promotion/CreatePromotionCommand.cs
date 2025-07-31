using ESApplication.Responses;
using ESDomain.AggregateModels.CategoryAggregate;
using MediatR; 
namespace ESApplication.Commands.Promotion
{
    public class CreatePromotionCommand : IRequest<Response<string>>
    {
        public string userid { get; set; }
        public string id { get; set; }
        public int isactive { get; set; }
        public int action { get; set; }
        public string couponcode { get; set; }
        public string description { get; set; }
        public string discount { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
    }
}

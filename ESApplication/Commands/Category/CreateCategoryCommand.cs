using ESApplication.Responses;
using ESDomain.AggregateModels.CategoryAggregate;
using MediatR; 
namespace ESApplication.Commands.Category
{
    public class CreateCategoryCommand : IRequest<Response<string>>
    {
        public string userid { get; set; }
        public string id { get; set; }
        public string code { get; set; }
        public string description { get; set; } 
        public string categorycode { get; set; }
        public int action { get; set; }
        public int isactive { get; set; } 
    }
}

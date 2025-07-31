using ESApplication.Responses;
using MediatR; 
namespace ESApplication.Commands.CommonData
{
    public class CreateCommonCommand : IRequest<Response<string>>
    {
        public string businessid { get; set; }
        public int count { get; set; }
        public string username { get; set; }
        public int type { get; set; }
    }
}

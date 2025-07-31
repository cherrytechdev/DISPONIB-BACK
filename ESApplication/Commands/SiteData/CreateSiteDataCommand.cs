using ESApplication.Responses;
using MediatR; 
namespace ESApplication.Commands.SiteData
{
    public class CreateSiteDataCommand : IRequest<Response<string>>
    {
        public string userid { get; set; } 
        public string type { get; set; }
        public string filename { get; set; }
        public string description { get; set; }
        public string base64text { get; set; }
        public List<string> fileNames { get; set; }
        public string filePath { get; set; }
    }
}

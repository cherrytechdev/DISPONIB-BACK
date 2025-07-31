
using ESApplication.Responses;
using ESDomain.AggregateModels.UserDetailsAggregate;
using MediatR;
using System.Text.Json.Serialization;
 

namespace ESApplication.Commands.SiteData
{
    public class DeleteSiteDataCommand : IRequest<Response<string>>
    {
        public string userid { get; set; }
        public string id { get; set; } 
    }
}

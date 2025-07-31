
using ESApplication.Responses;
using ESDomain.AggregateModels.UserDetailsAggregate;
using MediatR;
using System.Text.Json.Serialization;
 

namespace ESApplication.Commands.UserDetails
{
    public class UpdateUserDetailsCommand : IRequest<Response<string>>
    {
        public string userid { get; set; }
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }         
        public string email { get; set; }
        public string mobile { get; set; }
        public Int16 type { get; set; }
        public string businessid { get; set; }
        public string comment { get; set; }

        public Int16 isquiz { get; set; }

    }
}

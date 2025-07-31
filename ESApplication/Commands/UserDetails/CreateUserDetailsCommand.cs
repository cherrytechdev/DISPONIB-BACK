using ESApplication.Responses;
using MediatR; 
namespace ESApplication.Commands.UserDetails
{
    public class CreateUserDetailsCommand : IRequest<Response<string>>
    {
        public string username { get; set; }
        //public string roleid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string password { get; set; } 
        public string email { get; set; }
        public string mobile { get; set; } 
        public Int16 type { get; set; } 
        public string businessid { get; set; }
        public string comment { get; set; }
        public Int16 isquiz { get; set; } 

    }
}

using ESApplication.AggregateModels;
using ESApplication.Responses;
using MediatR; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESApplication.Queries.GetLogin
{
    public class GetLoginQuery : IRequest<List<UserDetailsDto>>
    {
        public string username { get; set; }

        public string password { get; set; }
    }
}

using ESApplication.AggregateModels;
using ESApplication.Responses;
using MediatR; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESApplication.Queries.GetUserDetails
{
    public class GetUserDetailsQuery : IRequest<List<UserDetailsDto>>
    {
        public string userid { get; set; }
    }
}

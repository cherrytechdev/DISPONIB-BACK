using ESApplication.AggregateModels;
using ESApplication.Responses;
using MediatR; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESApplication.Queries.GetRequesterAction
{
    public class GetRequesterActionQuery : IRequest<List<RequesterActionDto>>
    {
        public string referenceid { get; set; } 
    }
}

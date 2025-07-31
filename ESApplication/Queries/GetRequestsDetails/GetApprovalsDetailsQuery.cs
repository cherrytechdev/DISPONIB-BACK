using ESApplication.AggregateModels;
using ESApplication.Responses;
using MediatR; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESApplication.Queries.GetApprovalsDetails
{
    public class GetApprovalsDetailsQuery : IRequest<List<ApprovalsDetailsDto>>
    {
        public string referenceid { get; set; }

        public int permittype { get; set; }
    }
}

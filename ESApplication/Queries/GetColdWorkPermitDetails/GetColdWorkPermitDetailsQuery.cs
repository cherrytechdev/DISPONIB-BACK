using ESApplication.AggregateModels;
using ESApplication.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESApplication.Queries.GetColdWorkPermitDetails
{
    public class GetColdWorkPermitDetailsQuery : IRequest<List<ColdWorkPermitDetailDto>>
    {
        public string userid { get; set; }
        public int type { get; set; }
    }
}

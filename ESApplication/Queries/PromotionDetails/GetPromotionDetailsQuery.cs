using ESApplication.AggregateModels;
using ESApplication.Responses;
using MediatR; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESApplication.Queries.GetPromotion
{
    public class GetPromotionDetailsQuery : IRequest<List<PromotionDto>>
    {
        public string userid { get; set; }  
    }
}

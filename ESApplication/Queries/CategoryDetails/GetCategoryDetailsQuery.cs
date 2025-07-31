using ESApplication.AggregateModels;
using ESApplication.Responses;
using MediatR; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESApplication.Queries.GetCategory
{
    public class GetCategoryDetailsQuery : IRequest<List<CategoryDto>>
    {
        public string userid { get; set; } 
        public string code { get; set; }
    }
}

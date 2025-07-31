using ESApplication.AggregateModels;
using ESApplication.Responses;
using MediatR; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESApplication.Queries.GetDocumentsDetails
{
    public class GetDocumentsDetailsQuery : IRequest<List<DocumentsDetailsDto>>
    {
        public string referenceid { get; set; } 
    }
}

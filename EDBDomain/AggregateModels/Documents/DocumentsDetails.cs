
using ESDomain.SeedWork;
using Microsoft.AspNetCore.Http;

namespace ESDomain.AggregateModels.DocumentsDetailsAggregate
{
    public class DocumentsDetails
    {
        public int Type { get; set; }
        public string? FileName { get; set; }
        public string? FileSize { get; set; }
        public string? FileType { get; set; }
        public string? FileUrl { get; set; } 
    }
}

 

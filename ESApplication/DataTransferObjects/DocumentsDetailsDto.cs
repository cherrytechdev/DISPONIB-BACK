using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ESApplication.AggregateModels
{
    public class DocumentsDetailsDto
    {
        public int Type { get; set; }
        public string? FileName { get; set; }
        public string? FileSize { get; set; }
        public string? FileType { get; set; }
        public string? FileUrl { get; set; }
    }
}
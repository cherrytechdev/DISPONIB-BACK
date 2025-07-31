using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ESApplication.AggregateModels
{
    public class ApprovalsDetailsDto
    {  
        public Int64 id { get; set; }
        public string referenceid { get; set; }
        public int statusid { get; set; }
        public string status { get; set; }
        public int activityid { get; set; }
        public string activity { get; set; }
        public string comments { get; set; } 
        public DateTime actionon { get; set; } 
        public string actionby { get; set; }
        public DateTime modifiedon { get; set; }
        public string modifiedby { get; set; } 
    }
}
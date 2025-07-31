using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
namespace ESApplication.AggregateModels
{
    public class EmailConfigDto
    {
        public Int64 id { get; set; }
        public int sequence { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string email { get; set; }

    }
}

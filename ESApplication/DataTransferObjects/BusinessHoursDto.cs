
 
namespace ESApplication.AggregateModels
{
    public class BusinessHoursDto
    { 
        public string id { get; set; }
        public string day { get; set; }
        public DateTime starttime { get; set; }
        public DateTime endtime { get; set; } 
        public int isactive { get; set; } 
        public DateTime createdon { get; set; }
        public string status { get; set; }
    } 
}

 
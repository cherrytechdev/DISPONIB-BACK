
using ESDomain.SeedWork;

namespace ESDomain.AggregateModels.BusinessHoursAggregate
{
    public class BusinessHoursDetails : IAggregateRoot
    {
        public string userid { get; set; }
        public string id { get; set; }
        public string businessid { get; set; }
        public int isactive { get; set; }
        public int action { get; set; }
        public string day { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
        public Int16 status { get; set; }
    }
} 
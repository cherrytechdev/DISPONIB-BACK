
using ESDomain.SeedWork;

namespace ESDomain.AggregateModels.PromotionAggregate
{
    public class PromotionDetails : IAggregateRoot
    {
        public string userid { get; set; }
        public string id { get; set; }
        public int isactive { get; set; }
        public int action { get; set; }
        public string couponcode { get; set; }
        public string description { get; set; }
        public string discount { get; set; }        
        public string startdate { get; set; }
        public string enddate { get; set; }  
    }
} 
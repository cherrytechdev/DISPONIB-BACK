 
namespace ESApplication.AggregateModels
{
    public class PromotionDto
    { 
        public string id { get; set; }
        public string couponcode { get; set; }
        public string description { get; set; }
        public string discount { get; set; }
        public int isactive { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public DateTime createdon { get; set; }
    } 
}

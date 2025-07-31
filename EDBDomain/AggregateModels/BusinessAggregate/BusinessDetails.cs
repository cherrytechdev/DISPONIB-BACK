
using ESDomain.SeedWork;

namespace ESDomain.AggregateModels.BusinessAggregate
{
    public class BusinessDetails : IAggregateRoot
    {
        public string userid { get; set; }
        public string id { get; set; }
        public int isactive { get; set; }
        public int action { get; set; } 
        public string title { get; set; }
        public string description { get; set; }
        public string address { get; set; }
        public string categorycode { get; set; }
        public string subcategorycode { get; set; }
        public string mobile { get; set; }
        public string whatsapp { get; set; }
        public string email { get; set; }
        public string alternativeemail { get; set; }
        public string website { get; set; }
        public string instagram { get; set; }
        public string facebook { get; set; }
        public string linkedin { get; set; }
        public string couponcode { get; set; }  
    }
} 
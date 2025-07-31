 
namespace ESApplication.AggregateModels
{
    public class ReportsDto
    { 
        public Int64 id { get; set; }
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
        public int isactive { get; set; } 
        public DateTime createdon { get; set; }
        public string promodesc { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public string discount { get; set; }
        public string categorydesc { get; set; }
        public string subcategorydesc { get; set; }
        public Int16 likes { get; set; }
        public Int16 dislikes { get; set; }
        public Int16 ratings { get; set; }
        public Int16 views { get; set; }
        public Int16 reviews { get; set; }

    } 
}

 
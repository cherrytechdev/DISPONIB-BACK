
using ESDomain.SeedWork;

namespace ESDomain.AggregateModels.CategoryAggregate
{
    public class CategoryDetails : IAggregateRoot
    {
        public string userid { get; set; }
        public string id { get; set; }
        public string code { get; set; }
        public string description { get; set; } 
        public string categorycode { get; set; }
        public int action { get; set; }
        public int isactive { get; set; }   
    }
} 
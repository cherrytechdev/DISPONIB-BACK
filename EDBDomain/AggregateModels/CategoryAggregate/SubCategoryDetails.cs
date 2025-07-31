
using ESDomain.SeedWork;

namespace ESDomain.AggregateModels.CategoryAggregate
{
    public class SubCategoryDetails : IAggregateRoot
    {
        public Int64 userid { get; set; }
        public Int64 id { get; set; } 
        public string categorycode { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public int action { get; set; }
        public int isactive { get; set; }
    }
} 

using ESDomain.SeedWork;

namespace ESDomain.AggregateModels.CommonAggregate
{
    public class CommonDetails : IAggregateRoot
    {               
        public string businessid { get; set; }     
        public int count { get; set; }    
        public string username { get; set; }
        public int type { get; set; }

    }
} 

using ESDomain.SeedWork;

namespace ESDomain.AggregateModels.BusinessImageAggregate
{
    public class BusinessImageDetails : IAggregateRoot
    {              
        public string Id { get; set; }
        public string BusinessId { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public Int16 Status { get; set; }
        public int Isactive { get; set; }           
        public int Action { get; set; }
        public int IsDeleted { get; set; }
        public string UserId { get; set; }
        public int type { get; set; }


    }
} 
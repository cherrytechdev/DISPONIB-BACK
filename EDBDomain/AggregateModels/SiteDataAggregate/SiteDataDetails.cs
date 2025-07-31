
using ESDomain.SeedWork;

namespace ESDomain.AggregateModels.SiteDataAggregate
{
    public class SiteDataDetails : IAggregateRoot
    {
        public string userid { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string filename { get; set; }
        public string description { get; set; }
        public string base64text { get; set; }
        public string filepath { get; set; }
    }
} 
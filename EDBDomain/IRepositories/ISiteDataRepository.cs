

using ESDomain.AggregateModels.SiteDataAggregate;
using ESDomain.AggregateModels.UserDetailsAggregate;
using ESDomain.SeedWork;

namespace ESDomain.IRepositories
{
    public interface ISiteDataRepository : IRepository<SiteDataDetails>
    {
        SiteDataDetails Add(SiteDataDetails siteDataDetails);
        SiteDataDetails Update(SiteDataDetails siteDataDetails);
        SiteDataDetails Delete(SiteDataDetails siteDataDetails);
        SiteDataDetails Upload(SiteDataDetails siteDataDetails);
    }
}
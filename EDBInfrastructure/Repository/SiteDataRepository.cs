using ESDomain.AggregateModels.BusinessImageAggregate;
using ESDomain.AggregateModels.SiteDataAggregate;
using ESDomain.AggregateModels.UserDetailsAggregate;
using ESDomain.IRepositories;
using ESDomain.SeedWork;
using ESInfrastructure.DBContext; 

namespace ESInfrastructure.Repository
{
    public class SiteDataRepository : ISiteDataRepository
    {
        private readonly SiteDataDbContext unitofWork;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return unitofWork;
            }
        }

        public SiteDataRepository(SiteDataDbContext context)
        {
            this.unitofWork = context ?? throw new ArgumentNullException(nameof(context));
        }

        public SiteDataDetails Add(SiteDataDetails siteDataDetails)
        {
            return Manage(siteDataDetails);
        }
        public SiteDataDetails Update(SiteDataDetails siteDataDetails)
        {
            if (siteDataDetails != null)
            {
                this.unitofWork.siteDataDetails = siteDataDetails;
            }
            return this.unitofWork.siteDataDetails;
        }

        public SiteDataDetails Delete(SiteDataDetails siteDataDetails)
        {
            return Manage(siteDataDetails);
        }

        private SiteDataDetails Manage(SiteDataDetails siteDataDetails)
        {
            this.unitofWork.siteDataDetails = siteDataDetails;
            return this.unitofWork.siteDataDetails;
        }
        public SiteDataDetails Upload(SiteDataDetails siteDataDetails)
        {
            if (siteDataDetails != null)
            {
                this.unitofWork.siteDataDetails = siteDataDetails;
            }
            return this.unitofWork.siteDataDetails;
        }
    }
}

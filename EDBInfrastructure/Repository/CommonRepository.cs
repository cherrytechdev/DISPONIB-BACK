using ESDomain.AggregateModels.CommonAggregate;
using ESDomain.AggregateModels.SiteDataAggregate;
using ESDomain.AggregateModels.UserDetailsAggregate;
using ESDomain.IRepositories;
using ESDomain.SeedWork;
using ESInfrastructure.DBContext; 

namespace ESInfrastructure.Repository
{
    public class CommonRepository : ICommonRepository
    {
        private readonly CommonDbContext unitofWork;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return unitofWork;
            }
        }

        public CommonRepository(CommonDbContext context)
        {
            this.unitofWork = context ?? throw new ArgumentNullException(nameof(context));
        }

        public CommonDetails Add(CommonDetails CommonDetails)
        {
            return Manage(CommonDetails);
        }
        public CommonDetails Update(CommonDetails CommonDetails)
        {
            if (CommonDetails != null)
            {
                this.unitofWork.commonDetails = CommonDetails;
            }
            return this.unitofWork.commonDetails;
        }

        public CommonDetails Delete(CommonDetails CommonDetails)
        {
            return Manage(CommonDetails);
        }

        private CommonDetails Manage(CommonDetails CommonDetails)
        {
            this.unitofWork.commonDetails = CommonDetails;
            return this.unitofWork.commonDetails;
        }
    }
}

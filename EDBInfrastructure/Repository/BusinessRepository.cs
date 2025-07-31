
using ESDomain.AggregateModels.BusinessAggregate;
using ESDomain.IRepositories;
using ESDomain.SeedWork;
using ESInfrastructure.DBContext; 

namespace ESInfrastructure.Repository
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly BusinessDbContext unitofWork;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return unitofWork;
            }
        }

        public BusinessRepository(BusinessDbContext context)
        {
            this.unitofWork = context ?? throw new ArgumentNullException(nameof(context));
        }

        public BusinessDetails Add(BusinessDetails businessDetails)
        {
            return Manage(businessDetails);
        }
        public BusinessDetails Update(BusinessDetails businessDetails)
        {
            if (businessDetails != null)
            {
                this.unitofWork.businessDetails = businessDetails;
            }
            return this.unitofWork.businessDetails;
        }

        public BusinessDetails Delete(BusinessDetails businessDetails)
        {
            return Manage(businessDetails);
        }

        private BusinessDetails Manage(BusinessDetails businessDetails)
        {
            this.unitofWork.businessDetails = businessDetails;
            return this.unitofWork.businessDetails;
        }
    }
}

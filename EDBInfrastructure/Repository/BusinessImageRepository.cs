
using ESDomain.AggregateModels.BusinessHoursAggregate;
using ESDomain.AggregateModels.BusinessImageAggregate;
using ESDomain.IRepositories;
using ESDomain.SeedWork;
using ESInfrastructure.DBContext; 

namespace ESInfrastructure.Repository
{
    public class BusinessImageRepository : IBusinessImageRepository
    {
        private readonly BusinessImagesDbContext unitofWork;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return unitofWork;
            }
        }        

        public BusinessImageRepository(BusinessImagesDbContext context)
        {
            this.unitofWork = context ?? throw new ArgumentNullException(nameof(context));
        }


        private BusinessImageDetails Manage(BusinessImageDetails businessImageDetails)
        {
            this.unitofWork.businessImageDetails = businessImageDetails;
            return this.unitofWork.businessImageDetails;
        }

        public BusinessImageDetails  Upload(BusinessImageDetails businessImageDetails)
        {
            if (businessImageDetails != null)
            {
                this.unitofWork.businessImageDetails = businessImageDetails;
            }
            return this.unitofWork.businessImageDetails;
        }

        public BusinessImageDetails Delete(BusinessImageDetails businessImageDetails)
        {
            return Manage(businessImageDetails);
        }
    }
}

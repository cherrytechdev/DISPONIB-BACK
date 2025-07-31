
using ESDomain.AggregateModels.BusinessHoursAggregate;
using ESDomain.IRepositories;
using ESDomain.SeedWork;
using ESInfrastructure.DBContext; 

namespace ESInfrastructure.Repository
{
    public class BusinessHoursRepository : IBusinessHoursRepository
    {
        private readonly BusinessHoursDbContext unitofWork;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return unitofWork;
            }
        }

        public BusinessHoursRepository(BusinessHoursDbContext context)
        {
            this.unitofWork = context ?? throw new ArgumentNullException(nameof(context));
        }

        public BusinessHoursDetails Add(BusinessHoursDetails businessHoursDetails)
        {
            return Manage(businessHoursDetails);
        }
        public BusinessHoursDetails Update(BusinessHoursDetails businessHoursDetails)
        {
            if (businessHoursDetails != null)
            {
                this.unitofWork.businessHoursDetails = businessHoursDetails;
            }
            return this.unitofWork.businessHoursDetails;
        }

        public BusinessHoursDetails Delete(BusinessHoursDetails businessHoursDetails)
        {
            return Manage(businessHoursDetails);
        }

        private BusinessHoursDetails Manage(BusinessHoursDetails businessHoursDetails)
        {
            this.unitofWork.businessHoursDetails = businessHoursDetails;
            return this.unitofWork.businessHoursDetails;
        }
    }
}

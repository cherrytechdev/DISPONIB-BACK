using ESDomain.AggregateModels.EmployeeDetailAggregate;
using ESDomain.IRepositories;
using ESDomain.SeedWork;
using ESInfrastructure.DBContext; 

namespace ESInfrastructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext unitofWork;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return unitofWork;
            }
        }

        public EmployeeRepository(EmployeeDbContext context)
        {
            this.unitofWork = context ?? throw new ArgumentNullException(nameof(context));
        }

        public EmployeeDetail Add(EmployeeDetail userDetails)
        {
            return Manage(userDetails);
        }
        public EmployeeDetail Update(EmployeeDetail userDetails)
        {
            if (userDetails != null)
            {
                this.unitofWork.userDetails = userDetails;
            }
            return this.unitofWork.userDetails;
        }

        public EmployeeDetail Delete(EmployeeDetail userDetails)
        {
            return Manage(userDetails);
        }

        private EmployeeDetail Manage(EmployeeDetail userDetails)
        {
            this.unitofWork.userDetails = userDetails;
            return this.unitofWork.userDetails;
        }
    }
}

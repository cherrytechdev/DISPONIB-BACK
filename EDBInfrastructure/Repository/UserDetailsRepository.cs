using ESDomain.AggregateModels.UserDetailsAggregate;
using ESDomain.IRepositories;
using ESDomain.SeedWork;
using ESInfrastructure.DBContext; 

namespace ESInfrastructure.Repository
{
    public class UserDetailsRepository : IUserDetailsRepository
    {
        private readonly UserDetailsDbContext unitofWork;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return unitofWork;
            }
        }

        public UserDetailsRepository(UserDetailsDbContext context)
        {
            this.unitofWork = context ?? throw new ArgumentNullException(nameof(context));
        }

        public UserDetail Add(UserDetail userDetails)
        {
            return Manage(userDetails);
        }
        public UserDetail Update(UserDetail userDetails)
        {
            if (userDetails != null)
            {
                this.unitofWork.userDetails = userDetails;
            }
            return this.unitofWork.userDetails;
        }

        public UserDetail Delete(UserDetail userDetails)
        {
            return Manage(userDetails);
        }

        private UserDetail Manage(UserDetail userDetails)
        {
            this.unitofWork.userDetails = userDetails;
            return this.unitofWork.userDetails;
        }
    }
}

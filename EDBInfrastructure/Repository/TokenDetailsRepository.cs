using ESDomain.AggregateModels.TokenDetailsAggregate;
using ESDomain.AggregateModels.UserDetailsAggregate;
using ESDomain.IRepositories;
using ESDomain.SeedWork;
using ESInfrastructure.DBContext; 

namespace ESInfrastructure.Repository
{
    public class TokenDetailsRepository : ITokenDetailsRepository
    {
        private readonly TokenDetailsDbContext unitofWork;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return unitofWork;
            }
        }

        public TokenDetailsRepository(TokenDetailsDbContext context)
        {
            this.unitofWork = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TokenDetail Add(TokenDetail tokenDetails)
        {
            return Manage(tokenDetails);
        }
        public TokenDetail Update(TokenDetail tokenDetails)
        {
            if (tokenDetails != null)
            {
                this.unitofWork.tokenDetails = tokenDetails;
            }
            return this.unitofWork.tokenDetails;
        }

        public TokenDetail Delete(TokenDetail tokenDetails)
        {
            return Manage(tokenDetails);
        }

        private TokenDetail Manage(TokenDetail tokenDetails)
        {
            this.unitofWork.tokenDetails = tokenDetails;
            return this.unitofWork.tokenDetails;
        }
    }
}

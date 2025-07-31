using ESDomain.AggregateModels.CategoryAggregate;
using ESDomain.AggregateModels.PromotionAggregate;
using ESDomain.AggregateModels.UserDetailsAggregate;
using ESDomain.IRepositories;
using ESDomain.SeedWork;
using ESInfrastructure.DBContext; 

namespace ESInfrastructure.Repository
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly PromotionDbContext unitofWork;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return unitofWork;
            }
        }

        public PromotionRepository(PromotionDbContext context)
        {
            this.unitofWork = context ?? throw new ArgumentNullException(nameof(context));
        }

        public PromotionDetails Add(PromotionDetails promotionDetails)
        {
            return Manage(promotionDetails);
        }
        public PromotionDetails Update(PromotionDetails promotionDetails)
        {
            if (promotionDetails != null)
            {
                this.unitofWork.promotionDetails = promotionDetails;
            }
            return this.unitofWork.promotionDetails;
        }

        public PromotionDetails Delete(PromotionDetails promotionDetails)
        {
            return Manage(promotionDetails);
        }

        private PromotionDetails Manage(PromotionDetails promotionDetails)
        {
            this.unitofWork.promotionDetails = promotionDetails;
            return this.unitofWork.promotionDetails;
        }
    }
}

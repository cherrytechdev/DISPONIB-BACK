

using ESDomain.AggregateModels.CategoryAggregate;
using ESDomain.AggregateModels.PromotionAggregate;
using ESDomain.SeedWork;

namespace ESDomain.IRepositories
{
    public interface IPromotionRepository : IRepository<PromotionDetails>
    {
        PromotionDetails Add(PromotionDetails promotionDetails);
        PromotionDetails Update(PromotionDetails promotionDetails);
        PromotionDetails Delete(PromotionDetails promotionDetails);
    }
}
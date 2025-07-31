

using ESDomain.AggregateModels.CategoryAggregate;
using ESDomain.SeedWork;

namespace ESDomain.IRepositories
{
    public interface ICategoryRepository : IRepository<CategoryDetails>
    {
        CategoryDetails Add(CategoryDetails categoryDetails);
        CategoryDetails Update(CategoryDetails categoryDetails);
        CategoryDetails Delete(CategoryDetails categoryDetails);
    }
}
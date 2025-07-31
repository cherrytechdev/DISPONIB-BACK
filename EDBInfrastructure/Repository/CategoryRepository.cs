using ESDomain.AggregateModels.CategoryAggregate;
using ESDomain.AggregateModels.UserDetailsAggregate;
using ESDomain.IRepositories;
using ESDomain.SeedWork;
using ESInfrastructure.DBContext; 

namespace ESInfrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryDbContext unitofWork;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return unitofWork;
            }
        }

        public CategoryRepository(CategoryDbContext context)
        {
            this.unitofWork = context ?? throw new ArgumentNullException(nameof(context));
        }

        public CategoryDetails Add(CategoryDetails categoryDetails)
        {
            return Manage(categoryDetails);
        }
        public CategoryDetails Update(CategoryDetails categoryDetails)
        {
            if (categoryDetails != null)
            {
                this.unitofWork.categoryDetails = categoryDetails;
            }
            return this.unitofWork.categoryDetails;
        }

        public CategoryDetails Delete(CategoryDetails categoryDetails)
        {
            return Manage(categoryDetails);
        }

        private CategoryDetails Manage(CategoryDetails categoryDetails)
        {
            this.unitofWork.categoryDetails = categoryDetails;
            return this.unitofWork.categoryDetails;
        }
    }
}

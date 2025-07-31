

using ESDomain.AggregateModels.BusinessAggregate; 
using ESDomain.SeedWork;

namespace ESDomain.IRepositories
{
    public interface IBusinessRepository : IRepository<BusinessDetails>
    {
        BusinessDetails Add(BusinessDetails businessDetails);
        BusinessDetails Update(BusinessDetails businessDetails);
        BusinessDetails Delete(BusinessDetails businessDetails);
    }
}
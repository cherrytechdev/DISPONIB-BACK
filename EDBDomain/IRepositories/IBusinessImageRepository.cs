
 
using ESDomain.AggregateModels.BusinessHoursAggregate;
using ESDomain.AggregateModels.BusinessImageAggregate;
using ESDomain.SeedWork;

namespace ESDomain.IRepositories
{
    public interface IBusinessImageRepository : IRepository<BusinessImageDetails>
    {
        BusinessImageDetails Upload(BusinessImageDetails businessImageDetails);       
        BusinessImageDetails Delete(BusinessImageDetails businessImageDetails);
    }
}
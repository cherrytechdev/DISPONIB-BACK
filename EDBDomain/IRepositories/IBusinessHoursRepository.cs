
 
using ESDomain.AggregateModels.BusinessHoursAggregate;
using ESDomain.SeedWork;

namespace ESDomain.IRepositories
{
    public interface IBusinessHoursRepository : IRepository<BusinessHoursDetails>
    {
        BusinessHoursDetails Add(BusinessHoursDetails businessHoursDetails);
        BusinessHoursDetails Update(BusinessHoursDetails businessHoursDetails);
        BusinessHoursDetails Delete(BusinessHoursDetails businessHoursDetails);
    }
}


using ESDomain.AggregateModels.CommonAggregate;
using ESDomain.AggregateModels.SiteDataAggregate;
using ESDomain.AggregateModels.UserDetailsAggregate;
using ESDomain.SeedWork;

namespace ESDomain.IRepositories
{
    public interface ICommonRepository : IRepository<CommonDetails>
    {
        CommonDetails Add(CommonDetails commonDetails);
        CommonDetails Update(CommonDetails commonDetails);
        CommonDetails Delete(CommonDetails commonDetails);
    }
}
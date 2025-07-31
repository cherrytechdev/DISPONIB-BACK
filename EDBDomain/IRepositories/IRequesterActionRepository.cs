

using ESDomain.AggregateModels.RequesterActionAggregate;
using ESDomain.SeedWork;

namespace ESDomain.IRepositories
{
    public interface IRequesterActionRepository : IRepository<RequesterActionDetail>
    {
        RequesterActionDetail Add(RequesterActionDetail childrenDetail);
        UpdateRequesterActionDetail Update(UpdateRequesterActionDetail childrenDetail);
        RequesterActionDetail Delete(RequesterActionDetail childrenDetail);
    }
}

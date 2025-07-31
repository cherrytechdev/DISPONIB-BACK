

using ESDomain.AggregateModels.UserDetailsAggregate;
using ESDomain.SeedWork;

namespace ESDomain.IRepositories
{
    public interface IUserDetailsRepository : IRepository<UserDetail>
    {
        UserDetail Add(UserDetail userDetails);
        UserDetail Update(UserDetail userDetails);
        UserDetail Delete(UserDetail userDetails);
    }
}
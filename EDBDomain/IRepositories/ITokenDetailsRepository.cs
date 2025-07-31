

using ESDomain.AggregateModels.TokenDetailsAggregate;
using ESDomain.SeedWork;

namespace ESDomain.IRepositories
{
    public interface ITokenDetailsRepository : IRepository<TokenDetail>
    {
        TokenDetail Add(TokenDetail tokenDetail);
        TokenDetail Update(TokenDetail tokenDetail);
        TokenDetail Delete(TokenDetail tokenDetail);
    }
}
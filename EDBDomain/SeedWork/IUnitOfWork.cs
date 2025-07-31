
namespace ESDomain.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<List<Guid>> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> DeleteRecordAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> UpdateRecordAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> UpdateEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
public interface IRepository<T> : IDisposable where T : class
{
    Task<IEnumerable<T>> GetAllItemsAsync(CancellationToken cancellationToken = default);
    Task<T> GetItemByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task CreateItemAsync(T newItem, CancellationToken cancellationToken = default);
    Task UpdateItemAsync(Guid id, T updatedItem, CancellationToken cancellationToken = default);
    Task DeleteItemAsync(Guid id, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

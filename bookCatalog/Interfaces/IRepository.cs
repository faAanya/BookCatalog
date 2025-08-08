public interface IRepository<T> : IDisposable where T : class
{
    Task<IEnumerable<T>> GetAllItems();
    Task<T> GetItemById(Guid id);
    Task CreateItem(T Item);
    Task UpdateItem(Guid id, T newItem);
    Task DeleteItem(Guid id);
    Task SaveChangesAsync();
}
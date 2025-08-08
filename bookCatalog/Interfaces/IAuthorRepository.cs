public interface IAuthorRepository : IDisposable
{
    Task<IEnumerable<Author>> GetAllAuthors();
    Task<Author> GetAuthorById(Guid id);
    Task CreateAuthor(CreateAuthorDTO IAuthor);
    Task UpdateAuthor(Guid id, Author newAuthor);
    Task DeleteAuthor(Guid id);
    Task SaveChangesAsync();
}

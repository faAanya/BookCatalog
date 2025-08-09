public interface IAuthorRepository : IDisposable
{
    Task<IEnumerable<AuthorDTO>> GetAllAuthorsAsync();
    Task<AuthorDTO> GetAuthorByIdAsync(Guid id);
    Task CreateAuthorAsync(AuthorDTO IAuthor);
    Task UpdateAuthorAsync(Guid id, AuthorDTO newAuthor);
    Task DeleteAuthorAsync(Guid id);
    Task SaveChangesAsync();
}

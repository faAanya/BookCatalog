public interface IAuthorRepository : IDisposable
{
    Task<IEnumerable<AuthorDTO>> GetAllAuthors();
    Task<AuthorDTO> GetAuthorById(Guid id);
    Task CreateAuthor(AuthorDTO IAuthor);
    Task UpdateAuthor(Guid id, AuthorDTO newAuthor);
    Task DeleteAuthor(Guid id);
    Task SaveChangesAsync();
}

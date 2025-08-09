public interface IBookRepository : IDisposable
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<BookDTO> GetBookByIdAsync(Guid id);
    Task CreateBookAsync(BookDTO IBookem);
    Task UpdateBookAsync(Guid id, BookDTO newBook);
    Task DeleteBookAsync(Guid id);
    Task SaveChangesAsync();
}
public interface IBookRepository : IDisposable
{
    Task<IEnumerable<Book>> GetAllBooks();
    Task<BookDTO> GetBookById(Guid id);
    Task CreateBook(BookDTO IBookem);
    Task UpdateBook(Guid id, BookDTO newBook);
    Task DeleteBook(Guid id);
    Task SaveChangesAsync();
}
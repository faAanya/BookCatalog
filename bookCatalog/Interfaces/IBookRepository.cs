public interface IBookRepository : IDisposable
{
    Task<IEnumerable<Book>> GetAllBooks();
    Task<Book> GetBookById(Guid id);
    Task CreateBook(CreateBookDTO IBookem);
    Task UpdateBook(Guid id, Book newBook);
    Task DeleteBook(Guid id);
    Task SaveChangesAsync();
}
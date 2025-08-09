
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.EntityFrameworkCore;

public class BookPostgreRepository : IBookRepository
{
    private BookCatalogDbContext _dbContext;
    public BookPostgreRepository(BookCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    private bool disposed = false;

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        var books = await _dbContext.Books
        .Include(b => b.Authors)
        .Include(b => b.Genres)
        .ToListAsync();
        return books;
    }

    public async Task<BookDTO> GetBookByIdAsync(Guid id)
    {
        var book = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);
        var bookDTO = BookMapper.BookToDTO(book);

        return bookDTO;
    }

    public async Task CreateBookAsync(BookDTO bookDTO)
    {
        var authors = await _dbContext.Authors
                            .Where(a => bookDTO.Authors.Contains(a.Id))
                            .ToListAsync();

        var genres = await _dbContext.Genres
                            .Where(g => bookDTO.Genres.Contains(g.Id))
                            .ToListAsync();

        var newBook = BookMapper.DTOtoBook(bookDTO);
        newBook.Authors.AddRange(authors);
        newBook.Genres.AddRange(genres);

        await _dbContext.Books.AddAsync(newBook);
    }

    public async Task UpdateBookAsync(Guid id, BookDTO updatedBook)
    {
        var bookToUpdate = _dbContext.Books.FirstOrDefault(b => b.Id == id);

        bookToUpdate.Title = updatedBook.Title;
        bookToUpdate.Description = updatedBook.Description;
        bookToUpdate.PageCount = updatedBook.PageCount;
        bookToUpdate.ISBN = updatedBook.ISBN;
        bookToUpdate.PublicationYear = updatedBook.PublicationYear;
        bookToUpdate.CoverImageUrl = updatedBook.CoverImageUrl;
    }

    public async Task DeleteBookAsync(Guid id)
    {
        await _dbContext.Books.Where(book => book.Id == id).ExecuteDeleteAsync();
    }


    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }


}
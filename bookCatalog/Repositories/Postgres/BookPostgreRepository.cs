using Microsoft.EntityFrameworkCore;
using System.Threading;

public class BookPostgreRepository : IRepository<BookDTO>
{
    private readonly BookCatalogDbContext _dbContext;
    private bool disposed = false;

    public BookPostgreRepository(BookCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<BookDTO>> GetAllItemsAsync(CancellationToken cancellationToken = default)
    {
        var books = await _dbContext.Books
            .Include(b => b.Authors)
            .Include(b => b.Genres)
            .ToListAsync(cancellationToken);

        return books.Select(BookMapper.BookToDTO).ToList();
    }

    public async Task<BookDTO> GetItemByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var book = await _dbContext.Books
            .Include(b => b.Authors)
            .Include(b => b.Genres)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        return BookMapper.BookToDTO(book);
    }

    public async Task CreateItemAsync(BookDTO newItem, CancellationToken cancellationToken = default)
    {
        var authors = await _dbContext.Authors
            .Where(a => newItem.Authors.Contains(a.Id))
            .ToListAsync(cancellationToken);

        var genres = await _dbContext.Genres
            .Where(g => newItem.Genres.Contains(g.Id))
            .ToListAsync(cancellationToken);

        var newBook = BookMapper.DTOtoBook(newItem);
        newBook.Authors.AddRange(authors);
        newBook.Genres.AddRange(genres);

        await _dbContext.Books.AddAsync(newBook, cancellationToken);
    }

    public async Task UpdateItemAsync(Guid id, BookDTO updatedItem, CancellationToken cancellationToken = default)
    {
        var bookToUpdate = await _dbContext.Books
            .Include(b => b.Authors)
            .Include(b => b.Genres)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        if (bookToUpdate == null)
            throw new KeyNotFoundException($"Book with id {id} not found");

        bookToUpdate.Title = updatedItem.Title;
        bookToUpdate.Description = updatedItem.Description;
        bookToUpdate.PageCount = updatedItem.PageCount;
        bookToUpdate.ISBN = updatedItem.ISBN;
        bookToUpdate.PublicationYear = updatedItem.PublicationYear;
        bookToUpdate.CoverImageUrl = updatedItem.CoverImageUrl;

        var authors = await _dbContext.Authors
            .Where(a => updatedItem.Authors.Contains(a.Id))
            .ToListAsync(cancellationToken);
        bookToUpdate.Authors.Clear();
        bookToUpdate.Authors.AddRange(authors);

        var genres = await _dbContext.Genres
            .Where(g => updatedItem.Genres.Contains(g.Id))
            .ToListAsync(cancellationToken);
        bookToUpdate.Genres.Clear();
        bookToUpdate.Genres.AddRange(genres);
    }

    public async Task DeleteItemAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var book = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        if (book != null)
        {
            _dbContext.Books.Remove(book);
        }
        else
        {
            throw new KeyNotFoundException($"Book with id {id} not found");
        }
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

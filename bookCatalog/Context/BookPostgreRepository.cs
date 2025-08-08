
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.EntityFrameworkCore;

public class BookPostgreRepository : IRepository<Book>
{
    private BookCatalogDbContext _dbContext;
    public BookPostgreRepository(BookCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    private bool disposed = false;

    public async Task<IEnumerable<Book>> GetAllItems()
    {
        var books = await _dbContext.Books
        .Include(b => b.Authors)
        .Include(b => b.Genres)
        .ToListAsync();
        return books;
    }

    public async Task<Book> GetItemById(Guid id)
    {
        Book? book = await _dbContext.Books
          .Include(b => b.Authors)
          .Include(b => b.Genres)
          .FirstOrDefaultAsync(b => b.Id == id);

        return book;
    }


    public async Task CreateItem(Book Item)
    {
        var authorGuids = Item.Authors.Select(a => a.Id).ToList();
        var genresGuids = Item.Genres.Select(g => g.Id).ToList();

        var authors = await _dbContext.Authors
                            .Where(a => authorGuids.Contains(a.Id))
                            .ToListAsync();

        var genres = await _dbContext.Genres
                            .Where(g => genresGuids.Contains(g.Id))
                            .ToListAsync();

        Item.Authors.AddRange(authors);
        Item.Genres.AddRange(genres);

        await _dbContext.Books.AddAsync(Item);
    }

    public async Task UpdateItem(Guid id, Book newItem)
    {
        //     var book = await _dbContext.Books
        //    .Include(b => b.Authors)
        //    .Include(b => b.Genres)
        //    .FirstOrDefaultAsync(b => b.Id == id);

        //     if (book != null)
        //     {
        //         book.Title = newItem.Title;
        //         book.Description = newItem.Description;
        //         book.ISBN = newItem.ISBN;
        //         book.PublicationYear = newItem.PublicationYear;
        //         book.CoverImageUrl = newItem.CoverImageUrl;
        //         book.PageCount = newItem.PageCount;

        //         book.Authors.Clear();
        //         var authors = await _dbContext.Authors
        //             .Where(a => newItem.Authors.Select(x => x.Id).Contains(a.Id))
        //             .ToListAsync();
        //         book.Authors.AddRange(authors);

        //         book.Genres.Clear();
        //         var genres = await _dbContext.Genres
        //             .Where(g => newItem.Genres.Select(x => x.Id).Contains(g.Id))
        //             .ToListAsync();
        //         book.Genres.AddRange(genres);
        //     }
    }
    public async Task DeleteItem(Guid id)
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
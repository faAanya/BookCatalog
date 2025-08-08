
using Microsoft.EntityFrameworkCore;

public class AuthorPostgreRepository : IRepository<Author>
{
    private BookCatalogDbContext _dbContext;
    public AuthorPostgreRepository(BookCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    private bool disposed = false;

    public async Task<IEnumerable<Author>> GetAllItems()
    {
        var authors = await _dbContext.Authors.ToListAsync();
        return authors;
    }

    public async Task<Author> GetItemById(Guid id)
    {
        Author? author = await _dbContext.Authors.FirstOrDefaultAsync(a => a.Id == id);
        return author;
    }


    public async Task CreateItem(Author Item)
    {
        await _dbContext.Authors.AddAsync(Item);
    }


    public async Task UpdateItem(Guid id, Author newItem)
    {
        //     var Author = await _dbContext.Authors
        //    .Include(b => b.Authors)
        //    .Include(b => b.Genres)
        //    .FirstOrDefaultAsync(b => b.Id == id);

        //     if (Author != null)
        //     {
        //         Author.Title = newItem.Title;
        //         Author.Description = newItem.Description;
        //         Author.ISBN = newItem.ISBN;
        //         Author.PublicationYear = newItem.PublicationYear;
        //         Author.CoverImageUrl = newItem.CoverImageUrl;
        //         Author.PageCount = newItem.PageCount;

        //         Author.Authors.Clear();
        //         var authors = await _dbContext.Authors
        //             .Where(a => newItem.Authors.Select(x => x.Id).Contains(a.Id))
        //             .ToListAsync();
        //         Author.Authors.AddRange(authors);

        //         Author.Genres.Clear();
        //         var genres = await _dbContext.Genres
        //             .Where(g => newItem.Genres.Select(x => x.Id).Contains(g.Id))
        //             .ToListAsync();
        //         Author.Genres.AddRange(genres);
        //     }
    }
    public async Task DeleteItem(Guid id)
    {
        await _dbContext.Authors.Where(Author => Author.Id == id).ExecuteDeleteAsync();
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
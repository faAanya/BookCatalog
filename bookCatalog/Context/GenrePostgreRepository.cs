
using Microsoft.EntityFrameworkCore;

public class GenrePostgreRepository : IRepository<Genre>
{
    private BookCatalogDbContext _dbContext;
    public GenrePostgreRepository(BookCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    private bool disposed = false;

    public async Task<IEnumerable<Genre>> GetAllItems()
    {
        var genres = await _dbContext.Genres.ToListAsync();
        return genres;
    }

    public async Task<Genre> GetItemById(Guid id)
    {
        var genre = await _dbContext.Genres.FirstOrDefaultAsync(g => g.Id == id);
        return genre;
    }


    public async Task CreateItem(Genre Item)
    {
        await _dbContext.Genres.AddAsync(Item);
    }


    public async Task UpdateItem(Guid id, Genre newItem) { }
    public async Task DeleteItem(Guid id)
    {
        await _dbContext.Genres.Where(Genre => Genre.Id == id).ExecuteDeleteAsync();
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
using Microsoft.EntityFrameworkCore;
using System.Threading;

public class GenrePostgreRepository : IRepository<GenreDTO>
{
    private readonly BookCatalogDbContext _dbContext;
    private bool disposed = false;

    public GenrePostgreRepository(BookCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<GenreDTO>> GetAllItemsAsync(CancellationToken cancellationToken = default)
    {
        var genres = await _dbContext.Genres.ToListAsync(cancellationToken);
        return genres.Select(GenreMapper.GenreToDTO);
    }

    public async Task<GenreDTO> GetItemByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var genre = await _dbContext.Genres.FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
        if (genre == null)
            throw new KeyNotFoundException($"Genre with id {id} not found");

        return GenreMapper.GenreToDTO(genre);
    }

    public async Task CreateItemAsync(GenreDTO newItem, CancellationToken cancellationToken = default)
    {
        var newGenre = GenreMapper.DTOtoGenre(newItem);
        await _dbContext.Genres.AddAsync(newGenre, cancellationToken);
    }

    public async Task UpdateItemAsync(Guid id, GenreDTO updatedItem, CancellationToken cancellationToken = default)
    {
        var genre = await _dbContext.Genres.FindAsync(new object[] { id }, cancellationToken);
        if (genre == null)
            throw new KeyNotFoundException($"Genre with id {id} not found");

        genre.Name = updatedItem.Name;
        genre.Description = updatedItem.Description;
    }

    public async Task DeleteItemAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var genre = await _dbContext.Genres.FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
        if (genre == null)
            throw new KeyNotFoundException($"Genre with id {id} not found");

        _dbContext.Genres.Remove(genre);
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

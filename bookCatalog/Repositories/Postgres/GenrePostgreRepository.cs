
using Microsoft.EntityFrameworkCore;

public class GenrePostgreRepository : IGenreRepository
{
    private BookCatalogDbContext _dbContext;
    public GenrePostgreRepository(BookCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    private bool disposed = false;

    public async Task<IEnumerable<Genre>> GetAllGenres()
    {
        var genres = await _dbContext.Genres.ToListAsync();
        return genres;
    }


    public async Task<Genre> GetGenreById(Guid id)
    {
        var genre = await _dbContext.Genres.FirstOrDefaultAsync(g => g.Id == id);
        return genre;
    }

    public async Task CreateGenre(CreateGenreDTO genreDTO)
    {
        var newGenre = new Genre()
        {
            Name = genreDTO.Name,
            Description = genreDTO.Description,
        };
        newGenre.Books = new();
        await _dbContext.Genres.AddAsync(newGenre);
    }

    public async Task UpdateGenre(Guid id, Genre updatedGenre)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteGenre(Guid id)
    {
        await _dbContext.Genres.Where(genre => genre.Id == id).ExecuteDeleteAsync();
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
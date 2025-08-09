using Microsoft.EntityFrameworkCore;

public class GenrePostgreRepository : IGenreRepository
{
    private readonly BookCatalogDbContext _dbContext;
    private bool disposed = false;

    public GenrePostgreRepository(BookCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<GenreDTO>> GetAllGenres()
    {
        var genres = await _dbContext.Genres.ToListAsync();
        return genres.Select(GenreMapper.GenreToDTO);
    }

    public async Task<GenreDTO> GetGenreById(Guid id)
    {
        var genre = await _dbContext.Genres.FirstOrDefaultAsync(g => g.Id == id);
        return GenreMapper.GenreToDTO(genre);
    }

    public async Task CreateGenre(GenreDTO genreDTO)
    {
        var newGenre = GenreMapper.DTOtoGenre(genreDTO);
        await _dbContext.Genres.AddAsync(newGenre);
    }

    public async Task UpdateGenre(Guid id, GenreDTO updatedGenreDTO)
    {
        var genre = await _dbContext.Genres.FindAsync(id);
        genre.Name = updatedGenreDTO.Name;
        genre.Description = updatedGenreDTO.Description;
    }

    public async Task DeleteGenre(Guid id)
    {
        await _dbContext.Genres
            .Where(genre => genre.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public virtual void Dispose(bool disposing)
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

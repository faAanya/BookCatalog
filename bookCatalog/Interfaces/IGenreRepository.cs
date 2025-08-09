public interface IGenreRepository : IDisposable
{
    Task<IEnumerable<GenreDTO>> GetAllGenresAsync();
    Task<GenreDTO> GetGenreByIdAsync(Guid id);
    Task CreateGenreAsync(GenreDTO genreDTO);
    Task UpdateGenreAsync(Guid id, GenreDTO updatedGenreDTO);
    Task DeleteGenreAsync(Guid id);
    Task SaveChangesAsync();
}

public interface IGenreRepository : IDisposable
{
    Task<IEnumerable<GenreDTO>> GetAllGenres();
    Task<GenreDTO> GetGenreById(Guid id);
    Task CreateGenre(GenreDTO genreDTO);
    Task UpdateGenre(Guid id, GenreDTO updatedGenreDTO);
    Task DeleteGenre(Guid id);
    Task SaveChangesAsync();
}

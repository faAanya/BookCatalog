public interface IGenreRepository : IDisposable
{
    Task<IEnumerable<Genre>> GetAllGenres();
    Task<Genre> GetGenreById(Guid id);
    Task CreateGenre(CreateGenreDTO genreDTO);
    Task UpdateGenre(Guid id, Genre updatedGenre);
    Task DeleteGenre(Guid id);
    Task SaveChangesAsync();
}
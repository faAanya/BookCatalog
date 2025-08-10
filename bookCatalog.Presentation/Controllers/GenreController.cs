using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class GenresController : ControllerBase
{
    private readonly IRepository<GenreDTO> _dbContext;

    public GenresController(IRepository<GenreDTO> dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: /genres
    [HttpGet]
    public async Task<IActionResult> GetAllGenres(CancellationToken cancellationToken)
    {
        var genres = await _dbContext.GetAllItemsAsync(cancellationToken);
        return Ok(genres);
    }

    // GET: api/genres/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGenreById(Guid id, CancellationToken cancellationToken)
    {
        var genre = await _dbContext.GetItemByIdAsync(id, cancellationToken);
        if (genre == null)
            return NotFound();

        return Ok(genre);
    }

    // POST: api/genres
    [HttpPost]
    public async Task<IActionResult> CreateGenre([FromBody] GenreDTO newGenre, CancellationToken cancellationToken)
    {
        await _dbContext.CreateItemAsync(newGenre, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Ok(newGenre);
    }

    // PUT: api/genres/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGenre(Guid id, [FromBody] GenreDTO updatedGenre, CancellationToken cancellationToken)
    {
        await _dbContext.UpdateItemAsync(id, updatedGenre, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok();
    }

    // DELETE: api/genres/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(Guid id, CancellationToken cancellationToken)
    {
        await _dbContext.DeleteItemAsync(id, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}

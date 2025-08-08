using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class GenresController : ControllerBase
{
    private readonly IGenreRepository _dbContext;

    public GenresController(IGenreRepository dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: /authors
    [HttpGet]
    public async Task<IActionResult> GetAllGenres()
    {
        var genres = await _dbContext.GetAllGenres();
        return Ok(genres);
    }

    // GET: api/authors/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGenreById(Guid id)
    {
        var genre = await _dbContext.GetGenreById(id);
        if (genre == null)
            return NotFound();

        return Ok(genre);
    }

    // POST: api/books
    [HttpPost]
    public async Task<IActionResult> CreateGenre([FromBody] CreateGenreDTO newGenre)
    {
        await _dbContext.CreateGenre(newGenre);
        await _dbContext.SaveChangesAsync();
        return Ok(newGenre);
    }

    // PUT: api/books/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGenre(Guid id, [FromBody] Book updatedBook)
    {
        // await _dbContext.UpdateItem(id, updatedBook);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }

    // DELETE: api/books/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(Guid id)
    {
        await _dbContext.DeleteGenre(id);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
}

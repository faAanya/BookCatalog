using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class GenresController : ControllerBase
{
    private readonly IRepository<Genre> _dbContext;

    public GenresController(IRepository<Genre> dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: /authors
    [HttpGet]
    public async Task<IActionResult> GetAllGenres()
    {
        var genres = await _dbContext.GetAllItems();
        return Ok(genres);
    }

    // GET: api/authors/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGenreById(Guid id)
    {
        var genre = await _dbContext.GetItemById(id);
        if (genre == null)
            return NotFound();

        return Ok(genre);
    }

    // POST: api/books
    [HttpPost]
    public async Task<IActionResult> CreateGenre([FromBody] Genre newGenre)
    {
        await _dbContext.CreateItem(newGenre);
        await _dbContext.SaveChangesAsync();
        return CreatedAtAction(nameof(GetGenreById), new { id = newGenre.Id, title = newGenre.Name });
    }

    // PUT: api/books/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGenre(Guid id, [FromBody] Book updatedBook)
    {
        // await _dbContext.UpdateItem(id, updatedBook);
        // await _dbContext.SaveChangesAsync();

        return Ok();
    }

    // DELETE: api/books/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(Guid id)
    {
        await _dbContext.DeleteItem(id);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
}

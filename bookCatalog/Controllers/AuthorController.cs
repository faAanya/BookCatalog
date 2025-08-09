using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorRepository _dbContext;

    public AuthorsController(IAuthorRepository dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: /authors
    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        var author = await _dbContext.GetAllAuthors();
        return Ok(author);
    }

    // GET: api/authors/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthorById(Guid id)
    {
        var author = await _dbContext.GetAuthorById(id);
        if (author == null)
            return NotFound();

        return Ok(author);
    }

    // POST: api/authors
    [HttpPost]
    public async Task<IActionResult> CreateAuthor([FromBody] AuthorDTO newAuthor)
    {
        await _dbContext.CreateAuthor(newAuthor);
        await _dbContext.SaveChangesAsync();
        return Ok(newAuthor);
    }

    // PUT: api/authors/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody] AuthorDTO updatedAuthor)
    {
        await _dbContext.UpdateAuthor(id, updatedAuthor);
        await _dbContext.SaveChangesAsync();

        return Ok(updatedAuthor);
    }

    // DELETE: api/authors/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(Guid id)
    {
        await _dbContext.DeleteAuthor(id);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
}

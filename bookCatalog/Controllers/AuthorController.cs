using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IRepository<AuthorDTO> _dbContext;

    public AuthorsController(IRepository<AuthorDTO> dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: /authors
    [HttpGet]
    public async Task<IActionResult> GetAllAuthors(CancellationToken cancellationToken)
    {
        var author = await _dbContext.GetAllItemsAsync(cancellationToken);
        return Ok(author);
    }

    // GET: api/authors/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthorById(Guid id, CancellationToken cancellationToken)
    {
        var author = await _dbContext.GetItemByIdAsync(id, cancellationToken);
        if (author == null)
            return NotFound();

        return Ok(author);
    }

    // POST: api/authors
    [HttpPost]
    public async Task<IActionResult> CreateAuthor([FromBody] AuthorDTO newAuthor, CancellationToken cancellationToken)
    {
        await _dbContext.CreateItemAsync(newAuthor, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Ok(newAuthor);
    }

    // PUT: api/authors/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody] AuthorDTO updatedAuthor, CancellationToken cancellationToken)
    {
        await _dbContext.UpdateItemAsync(id, updatedAuthor, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok(updatedAuthor);
    }

    // DELETE: api/authors/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(Guid id, CancellationToken cancellationToken)
    {
        await _dbContext.DeleteItemAsync(id, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}

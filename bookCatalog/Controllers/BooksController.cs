using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly IRepository<BookDTO> _dbContext;

    public BooksController(IRepository<BookDTO> dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: api/books
    [HttpGet]
    public async Task<IActionResult> GetAllBooks(CancellationToken cancellationToken)
    {
        var books = await _dbContext.GetAllItemsAsync(cancellationToken);
        return Ok(books);
    }

    // GET: api/books/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(Guid id, CancellationToken cancellationToken)
    {
        var book = await _dbContext.GetItemByIdAsync(id, cancellationToken);
        if (book == null)
            return NotFound();

        return Ok(book);
    }

    // POST: api/books
    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] BookDTO book, CancellationToken cancellationToken)
    {
        await _dbContext.CreateItemAsync(book, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Ok(book);
    }

    // PUT: api/books/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(Guid id, [FromBody] BookDTO updatedBook, CancellationToken cancellationToken)
    {
        await _dbContext.UpdateItemAsync(id, updatedBook, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok(updatedBook);
    }

    // DELETE: api/books/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(Guid id, CancellationToken cancellationToken)
    {
        await _dbContext.DeleteItemAsync(id, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}

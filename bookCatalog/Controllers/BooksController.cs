using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly IRepository<Book> _dbContext;

    public BooksController(IRepository<Book> dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: api/books
    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _dbContext.GetAllItems();

        var result = books.Select(BookMapper.BookToDTO);

        return Ok(result);
    }

    // GET: api/books/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(Guid id)
    {
        var book = await _dbContext.GetItemById(id);

        if (book == null)
            return NotFound();

        return Ok(book);
    }

    // POST: api/books
    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] Book book)
    {
        await _dbContext.CreateItem(book);
        await _dbContext.SaveChangesAsync();
        return CreatedAtAction(nameof(GetBookById), new { id = book.Id, title = book.Title });
    }

    // PUT: api/books/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(Guid id, [FromBody] Book updatedBook)
    {
        await _dbContext.UpdateItem(id, updatedBook);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }

    // DELETE: api/books/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        await _dbContext.DeleteItem(id);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
}

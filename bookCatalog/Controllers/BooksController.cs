using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _dbContext;

    public BooksController(IBookRepository dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: api/books
    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _dbContext.GetAllBooksAsync();
        var result = books.Select(BookMapper.BookToDTO);

        return Ok(result);
    }

    // GET: api/books/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(Guid id)
    {
        var book = await _dbContext.GetBookByIdAsync(id);
        if (book == null)
            return NotFound();

        return Ok(book);
    }

    // POST: api/books
    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] BookDTO book)
    {
        await _dbContext.CreateBookAsync(book);
        await _dbContext.SaveChangesAsync();
        return Ok(book);
    }

    // PUT: api/books/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(Guid id, [FromBody] BookDTO updatedBook)
    {
        await _dbContext.UpdateBookAsync(id, updatedBook);
        await _dbContext.SaveChangesAsync();

        return Ok(updatedBook);
    }

    // DELETE: api/books/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        await _dbContext.DeleteBookAsync(id);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
}

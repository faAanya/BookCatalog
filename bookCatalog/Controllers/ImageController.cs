using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class ImagesController : ControllerBase
{
    private readonly IBookRepository _dbContext;
    private readonly FileService _fileService;

    public ImagesController(IBookRepository dbContext, FileService fileService)
    {
        _dbContext = dbContext;
        _fileService = fileService;
    }

    // GET: images/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetFile(Guid id)
    {
        BookDTO? book = await _dbContext.GetBookByIdAsync(id);

        var fileName = Path.GetFileName(new Uri(book.CoverImageUrl).AbsolutePath);

        var filePath = _fileService.GetFilePath(fileName);

        return PhysicalFile(filePath, "application/octet-stream", fileName);
    }

    // POST: /images
    [HttpPost]
    public async Task<IActionResult> DownloadImage([FromBody] ImageDTO request)
    {
        await _fileService.DownloadFile(request.FileName);
        return Ok(new { message = "File downloaded successfully." });
    }

}

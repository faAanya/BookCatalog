using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class ImagesController : ControllerBase
{
    private readonly IRepository<BookDTO> _dbContext;
    private readonly FileService _fileService;

    public ImagesController(IRepository<BookDTO> dbContext, FileService fileService)
    {
        _dbContext = dbContext;
        _fileService = fileService;
    }

    // GET: images/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetFile(Guid id, CancellationToken cancellationToken)
    {
        BookDTO? book = await _dbContext.GetItemByIdAsync(id, cancellationToken);

        var fileName = Path.GetFileName(new Uri(book.CoverImageUrl).AbsolutePath);

        var filePath = _fileService.GetFilePath(fileName);

        return PhysicalFile(filePath, "application/octet-stream", fileName);
    }

    // POST: /images
    [HttpPost]
    public async Task<IActionResult> DownloadImage([FromBody] ImageDTO request, CancellationToken cancellationToken)
    {
        await _fileService.DownloadFile(request.FileName, cancellationToken);
        return Ok(new { message = "File downloaded successfully." });
    }

}

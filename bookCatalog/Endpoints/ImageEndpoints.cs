using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class ImagesEndpoints
{
    public static RouteGroupBuilder MapImagesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("images");

        //gets book by id
        //GET /images/{id}
        group.MapGet("/{id}", async (Guid id, FileService fileService, BookCatalogDbContext dbContext) =>
         {
             Book? book = await dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);
             if (book == null)
                 return Results.NoContent();

             var fileName = Path.GetFileName(new Uri(book.CoverImageUrl).AbsolutePath);

             var filePath = fileService.GetFilePath(fileName);

             if (filePath == null)
                 return Results.NotFound();

             return Results.File(filePath, fileName);
         });

        //adds new book
        //POST /images
        group.MapPost("/", async ([FromBody] ImageDTO request, FileService fileService) =>
        {
            await fileService.DownloadFile(request.FileName);
        });

        return group;
    }
}

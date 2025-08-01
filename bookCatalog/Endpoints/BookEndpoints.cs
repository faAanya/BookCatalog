using Microsoft.EntityFrameworkCore;

public static class BooksEndpoints
{
    public static RouteGroupBuilder MapBooksEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("books");


        //gets all books
        //GET /books
        group.MapGet("/", async (BookCatalogDbContext dbContext) =>
        {
            var books = await dbContext.Books.ToListAsync();
            return Results.Ok(books);
        });

        //gets book by id
        //GET /books/{id}
        group.MapGet("/{id}", async (Guid bookId, BookCatalogDbContext dbContext) =>
       {
           Book? book = await dbContext.Books.FindAsync(bookId);

           return book == null ? Results.NotFound() : Results.Ok(book);

       });

        //adds new book
        //POST /books
        group.MapPost("/", async (Book newBook, BookCatalogDbContext dbContext) =>
        {
            await dbContext.AddAsync(newBook);
            await dbContext.SaveChangesAsync();

            return Results.Created($"/books/{newBook.Id}", newBook);
        });

        //changes the book with certain id
        //PUT /books/{id}
        group.MapPut("/{id}", async (Guid bookId, Book updatedBook, BookCatalogDbContext dbContext) =>
        {
            var bookToUpdate = await dbContext.Books.FindAsync(bookId);

            if (bookToUpdate == null)
                return Results.NotFound();

            dbContext.Entry(bookToUpdate).CurrentValues.SetValues(updatedBook);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        //deletes the book with certain id
        //DELETE /books/{id}
        group.MapDelete("/{id}", async (Guid bookId, BookCatalogDbContext dbContext) =>
        {
            await dbContext.Books.Where(book => book.Id == bookId).ExecuteDeleteAsync();
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        return group;
    }
}
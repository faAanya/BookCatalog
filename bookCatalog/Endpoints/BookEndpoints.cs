using Microsoft.AspNetCore.Mvc;
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
        group.MapGet("/{id}", async (Guid id, BookCatalogDbContext dbContext) =>
       {
           Book? book = await dbContext.Books.FindAsync(id);

           return book == null ? Results.NotFound() : Results.Ok(book);

       });

        //adds new book
        //POST /books
        group.MapPost("/", async ([FromBody] CreateBookDTO newBookDTO, BookCatalogDbContext dbContext) =>
        {
            var authorGuids = newBookDTO.Authors
       .Select(a => Guid.Parse(a.ToString())).ToList();

            var genresGuids = newBookDTO.Genres
                   .Select(g => Guid.Parse(g.ToString())).ToList();

            var authors = await dbContext.Authors
            .Where(a => authorGuids.Contains(a.Id))
            .ToListAsync();

            var genres = await dbContext.Genres
           .Where(g => genresGuids.Contains(g.Id))
           .ToListAsync();

            var newBook = new Book
            {
                Title = newBookDTO.Title,
                Description = newBookDTO.Description,
                ISBN = newBookDTO.ISBN,
                PublicationYear = newBookDTO.PublicationYear,
                CoverImageUrl = newBookDTO.CoverImageUrl,
                PageCount = newBookDTO.PageCount,
            };

            newBook.Authors.AddRange(authors);
            newBook.Genres.AddRange(genres);

            await dbContext.AddAsync(newBook);
            await dbContext.SaveChangesAsync();

            return Results.Created($"/books/{newBook.Id}", new
            {
                newBook.Id,
                newBook.Title,
                newBook.Description,
                newBook.ISBN,
                newBook.PublicationYear,
                newBook.CoverImageUrl,
                newBook.PageCount,
                Authors = authors.Select(a => new { a.Id, a.FirstName, a.LastName }),
                Genres = genres.Select(g => new { g.Id, g.Name })
            });
        });

        //changes the book with certain id
        //PUT /books/{id}
        group.MapPut("/{id}", async (Guid id, Book updatedBook, BookCatalogDbContext dbContext) =>
        {
            var bookToUpdate = await dbContext.Books.FindAsync(id);

            if (bookToUpdate == null)
                return Results.NotFound();

            dbContext.Entry(bookToUpdate).CurrentValues.SetValues(updatedBook);
            await dbContext.SaveChangesAsync();
            return Results.Ok(bookToUpdate);
        });

        //deletes the book with certain id
        //DELETE /books/{id}
        group.MapDelete("/{id}", async (Guid id, BookCatalogDbContext dbContext) =>
        {
            await dbContext.Books.Where(book => book.Id == id).ExecuteDeleteAsync();
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        return group;
    }
}
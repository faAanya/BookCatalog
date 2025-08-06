using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class AuthorsEndpoints
{
    public static RouteGroupBuilder MapAuthorsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("authors");

        //gets all authors
        //GET /authors
        group.MapGet("/", async (BookCatalogDbContext dbContext) =>
        {
            var authors = await dbContext.Authors.ToListAsync();
            return Results.Ok(authors);
        });

        //gets author by id
        //GET /Authors/{id}
        group.MapGet("/{id}", async (Guid id, BookCatalogDbContext dbContext) =>
       {
           Author? author = await dbContext.Authors.FindAsync(id);

           return author == null ? Results.NotFound() : Results.Ok(author);

       });

        //adds new author
        //POST /Authors
        group.MapPost("/", async (Author newAuthor, BookCatalogDbContext dbContext) =>
        {
            await dbContext.AddAsync(newAuthor);
            await dbContext.SaveChangesAsync();

            return Results.Created($"/authors/{newAuthor.Id}", newAuthor);
        });

        //changes the author with certain id
        //PUT /Authors/{id}
        group.MapPut("/{id}", async (Guid id, Author updatedauthor, BookCatalogDbContext dbContext) =>
        {
            var authorToUpdate = await dbContext.Authors.FindAsync(id);

            if (authorToUpdate == null)
                return Results.NotFound();

            dbContext.Entry(authorToUpdate).CurrentValues.SetValues(updatedauthor);
            await dbContext.SaveChangesAsync();
            return Results.Ok(authorToUpdate);
        });

        //deletes the author with certain id
        //DELETE /Authors/{id}
        group.MapDelete("/{id}", async (Guid id, BookCatalogDbContext dbContext) =>
        {
            await dbContext.Authors.Where(author => author.Id == id).ExecuteDeleteAsync();
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        return group;
    }
}
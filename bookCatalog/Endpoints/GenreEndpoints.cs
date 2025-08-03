using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class GenresEndpoints
{
    public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("genres");


        //gets all genres
        //GET /genres
        group.MapGet("/", async (BookCatalogDbContext dbContext) =>
        {
            var genres = await dbContext.Genres.ToListAsync();
            return Results.Ok(genres);
        });

        //gets genre by id
        //GET /genres/{id}
        group.MapGet("/{id}", async (Guid id, BookCatalogDbContext dbContext) =>
       {
           Genre? genre = await dbContext.Genres.FindAsync(id);

           return genre == null ? Results.NotFound() : Results.Ok(genre);

       });

        //adds new genre
        //POST /genres
        group.MapPost("/", async (Genre newgenre, BookCatalogDbContext dbContext) =>
        {
            await dbContext.AddAsync(newgenre);
            await dbContext.SaveChangesAsync();

            return Results.Created($"/genres/{newgenre.Id}", newgenre);
        });

        //changes the genre with certain id
        //PUT /genres/{id}
        group.MapPut("/{id}", async (Guid id, Genre updatedgenre, BookCatalogDbContext dbContext) =>
        {
            var genreToUpdate = await dbContext.Genres.FindAsync(id);

            if (genreToUpdate == null)
                return Results.NotFound();

            dbContext.Entry(genreToUpdate).CurrentValues.SetValues(updatedgenre);
            await dbContext.SaveChangesAsync();
            return Results.Ok(genreToUpdate);
        });

        //deletes the genre with certain id
        //DELETE /genres/{id}
        group.MapDelete("/{id}", async (Guid id, BookCatalogDbContext dbContext) =>
        {
            await dbContext.Genres.Where(genre => genre.Id == id).ExecuteDeleteAsync();
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        return group;
    }
}
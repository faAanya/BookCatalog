using Microsoft.EntityFrameworkCore;

public static class Migrations
{
    public static async void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<BookCatalogDbContext>();
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
            {
                System.Console.WriteLine("Applying pending migrations");
                await dbContext.Database.MigrateAsync();
                System.Console.WriteLine("Migrations applied succesfully");
            }
            else
            {
                System.Console.WriteLine("No pending migrations found.");
            }

        }

    }
}
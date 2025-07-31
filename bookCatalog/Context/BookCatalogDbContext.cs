using Microsoft.EntityFrameworkCore;

class BookCatalogDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public BookCatalogDbContext(DbContextOptions<BookCatalogDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}
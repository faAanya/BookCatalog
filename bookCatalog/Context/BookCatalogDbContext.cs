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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Book>()
                .HasMany(b => b.Authors)
                .WithMany(a => a.Books)
                .UsingEntity(j => j.ToTable("BookAuthors"));

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Genres)
            .WithMany(g => g.Books)
            .UsingEntity(j => j.ToTable("BookGenres"));

        modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = Guid.Parse("a1111111-1111-1111-1111-111111111111"),
                    FirstName = "George",
                    LastName = "Orwell",
                    Biography = "British writer known for '1984' and 'Animal Farm'"
                },
                new Author
                {
                    Id = Guid.Parse("a2222222-2222-2222-2222-222222222222"),
                    FirstName = "Jane",
                    LastName = "Austen",
                    Biography = "English novelist known for 'Pride and Prejudice'"
                },
                new Author
                {
                    Id = Guid.Parse("a3333333-3333-3333-3333-333333333333"),
                    FirstName = "Mark",
                    LastName = "Twain",
                    Biography = "American author of 'Adventures of Huckleberry Finn'"
                },
                new Author
                {
                    Id = Guid.Parse("a4444444-4444-4444-4444-444444444444"),
                    FirstName = "J.K.",
                    LastName = "Rowling",
                    Biography = "British author of the Harry Potter series"
                },
                new Author
                {
                    Id = Guid.Parse("a5555555-5555-5555-5555-555555555555"),
                    FirstName = "Stephen",
                    LastName = "King",
                    Biography = "American author known for horror novels"
                },
                new Author
                {
                    Id = Guid.Parse("a6666666-6666-6666-6666-666666666666"),
                    FirstName = "Ernest",
                    LastName = "Hemingway",
                    Biography = "American novelist and Nobel Prize winner"
                },
                new Author
                {
                    Id = Guid.Parse("a7777777-7777-7777-7777-777777777777"),
                    FirstName = "Agatha",
                    LastName = "Christie",
                    Biography = "British author famous for detective novels"
                },
                new Author
                {
                    Id = Guid.Parse("a8888888-8888-8888-8888-888888888888"),
                    FirstName = "Leo",
                    LastName = "Tolstoy",
                    Biography = "Russian writer known for 'War and Peace'"
                },
                new Author
                {
                    Id = Guid.Parse("a9999999-9999-9999-9999-999999999999"),
                    FirstName = "Fyodor",
                    LastName = "Dostoevsky",
                    Biography = "Russian novelist known for 'Crime and Punishment'"
                },
                new Author
                {
                    Id = Guid.Parse("abbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    FirstName = "Haruki",
                    LastName = "Murakami",
                    Biography = "Japanese author known for surreal and magical realism"
                }
            );

        modelBuilder.Entity<Genre>().HasData(
            new Genre
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Science Fiction",
                Description = "Books about fictional worlds and future technologies"
            },
            new Genre
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Romance",
                Description = "Narrative fiction focusing on relationships and love"
            },
            new Genre
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name = "Mystery",
                Description = "Stories focused on solving crimes and uncovering secrets"
            },
            new Genre
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Name = "Fantasy",
                Description = "Books featuring magic, mythical creatures, and imaginary worlds"
            },
            new Genre
            {
                Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                Name = "Biography",
                Description = "Books about the lives of real people"
            },
            new Genre
            {
                Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                Name = "Science",
                Description = "Popular and academic scientific works"
            },
            new Genre
            {
                Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                Name = "History",
                Description = "Books exploring past events and historical figures"
            },
            new Genre
            {
                Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                Name = "Psychology",
                Description = "Books about human behavior and mental processes"
            },
            new Genre
            {
                Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                Name = "Self-Help",
                Description = "Books about personal development and success"
            },
            new Genre
            {
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "Adventure",
                Description = "Exciting stories involving travel, exploration, and danger"
            }
        );
    }

}
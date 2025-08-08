
using Microsoft.EntityFrameworkCore;

public class AuthorPostgreRepository : IAuthorRepository
{
    private BookCatalogDbContext _dbContext;
    private bool disposed = false;
    public AuthorPostgreRepository(BookCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Author>> GetAllAuthors()
    {
        var authors = await _dbContext.Authors.ToListAsync();
        return authors;
    }

    public async Task<Author> GetAuthorById(Guid id)
    {
        Author? author = await _dbContext.Authors.FirstOrDefaultAsync(a => a.Id == id);
        return author;
    }

    public async Task CreateAuthor(CreateAuthorDTO authorDTO)
    {
        var newAuthor = new Author()
        {
            FirstName = authorDTO.FirstName,
            LastName = authorDTO.LastName,
            Biography = authorDTO.Biography
        };
        newAuthor.Books = new();
        await _dbContext.Authors.AddAsync(newAuthor);
    }

    public async Task UpdateAuthor(Guid id, Author newAuthor)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAuthor(Guid id)
    {
        await _dbContext.Authors.Where(author => author.Id == id).ExecuteDeleteAsync();
    }
    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

}
using Microsoft.EntityFrameworkCore;

public class AuthorPostgreRepository : IRepository<AuthorDTO>
{
    private readonly BookCatalogDbContext _dbContext;
    private bool disposed = false;

    public AuthorPostgreRepository(BookCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<AuthorDTO>> GetAllItemsAsync(CancellationToken cancellationToken = default)
    {
        var authors = await _dbContext.Authors.ToListAsync(cancellationToken);
        return authors.Select(AuthorMapper.AuthorToDTO).ToList();
    }

    public async Task<AuthorDTO> GetItemByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var author = await _dbContext.Authors.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        if (author == null)
            throw new KeyNotFoundException($"Author with id {id} not found");

        return AuthorMapper.AuthorToDTO(author);
    }

    public async Task CreateItemAsync(AuthorDTO newItem, CancellationToken cancellationToken = default)
    {
        var newAuthor = AuthorMapper.DTOtoAuthor(newItem);
        await _dbContext.Authors.AddAsync(newAuthor, cancellationToken);
    }

    public async Task UpdateItemAsync(Guid id, AuthorDTO updatedItem, CancellationToken cancellationToken = default)
    {
        var updatedAuthor = await _dbContext.Authors.FindAsync(new object[] { id }, cancellationToken);
        if (updatedAuthor == null)
            throw new KeyNotFoundException($"Author with id {id} not found");

        updatedAuthor.FirstName = updatedItem.FirstName;
        updatedAuthor.LastName = updatedItem.LastName;
        updatedAuthor.Biography = updatedItem.Biography;
    }

    public async Task DeleteItemAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var author = await _dbContext.Authors.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        if (author == null)
            throw new KeyNotFoundException($"Author with id {id} not found");

        _dbContext.Authors.Remove(author);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

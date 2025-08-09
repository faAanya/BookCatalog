
using Microsoft.EntityFrameworkCore;

public class AuthorPostgreRepository : IAuthorRepository
{
    private BookCatalogDbContext _dbContext;
    private bool disposed = false;
    public AuthorPostgreRepository(BookCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<AuthorDTO>> GetAllAuthors()
    {
        var authors = await _dbContext.Authors.ToListAsync();
        return authors.Select(AuthorMapper.AuthorToDTO).ToList();
    }

    public async Task<AuthorDTO> GetAuthorById(Guid id)
    {
        var author = await _dbContext.Authors.FirstOrDefaultAsync(a => a.Id == id);
        var authorDTO = AuthorMapper.AuthorToDTO(author);
        return authorDTO;
    }

    public async Task CreateAuthor(AuthorDTO authorDTO)
    {
        var newAuthor = AuthorMapper.DTOtoAuthor(authorDTO);
        await _dbContext.Authors.AddAsync(newAuthor);
    }

    public async Task UpdateAuthor(Guid id, AuthorDTO newAuthor)
    {
        var updatedAuthor = await _dbContext.Authors.FindAsync(id);
        updatedAuthor.FirstName = newAuthor.FirstName;
        updatedAuthor.LastName = newAuthor.LastName;
        updatedAuthor.Biography = newAuthor.Biography;
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
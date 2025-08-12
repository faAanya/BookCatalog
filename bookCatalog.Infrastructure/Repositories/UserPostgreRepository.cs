using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class UserPostgreRepository : IUserRepository
{
    private readonly BookCatalogDbContext _context;

    public UserPostgreRepository(BookCatalogDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
    }

    public async Task<bool> ExistsAsync(string username, CancellationToken cancellationToken)
    {
        return await _context.Users.AnyAsync(u => u.Username == username, cancellationToken);
    }
}

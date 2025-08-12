using Microsoft.AspNetCore.Identity;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher<User> _passwordHasher = new();

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> RegisterAsync(RegisterUserDto dto, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistsAsync(dto.Username, cancellationToken))
            return false;

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

        await _userRepository.AddAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<User?> ValidateUserAsync(LoginUserDto dto, CancellationToken cancellationToke)
    {
        var user = await _userRepository.GetByUsernameAsync(dto.Username, cancellationToke);
        if (user == null) return null;

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        return result == PasswordVerificationResult.Success ? user : null;
    }
}

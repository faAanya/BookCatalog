using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly TokenService _tokenService;

    public AuthController(UserService userService, TokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto, CancellationToken cancellationToken)
    {
        var result = await _userService.RegisterAsync(dto, cancellationToken);
        if (!result)
            return BadRequest("User already exists");

        return Ok("User registered");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto dto, CancellationToken cancellationToken)
    {
        var user = await _userService.ValidateUserAsync(dto, cancellationToken);
        if (user == null)
            return Unauthorized();

        var token = _tokenService.GenerateJwtToken(user.Username);
        return Ok(new { token });
    }
}

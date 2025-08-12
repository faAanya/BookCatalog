using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class TokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config)
    {
        _config = config;
    }
    public string GenerateJwtToken(string username)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtConfig:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
           issuer: _config["JwtConfig:Issuer"],
           audience: _config["JwtConfig:Audience"],
           claims: claims,
           expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["JwtConfig:TokenValidityMins"])),
           signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
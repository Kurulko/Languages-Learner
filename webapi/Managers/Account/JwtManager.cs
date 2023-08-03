using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Models.Database;
using WebApi.Services.Account;
using WebApi.Settings;

namespace WebApi.Managers.Account;

public class JwtManager : IJwtService
{
    readonly JwtSettings jwtSettings;
    public JwtManager(JwtSettings jwtSettings)
        => this.jwtSettings = jwtSettings;

    public (string token, int expirationDays) GenerateJwtToken(User user, IEnumerable<string> roles)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

        var rolesClaims = roles.Select(role => new Claim(ClaimTypes.Role, role));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName!),
            }.Union(rolesClaims)),
            Expires = DateTime.UtcNow.AddDays(jwtSettings.ExpirationDays),
            Issuer = jwtSettings.Issuer,
            Audience = jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return (tokenHandler.WriteToken(token), jwtSettings.ExpirationDays);
    }

    public ClaimsPrincipal GetPrincipalFromToken(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        var validationParameters = (TokenValidationParameters)jwtSettings;

        try
        {
            return tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
        }
        catch
        {
            return null!;
        }
    }
}

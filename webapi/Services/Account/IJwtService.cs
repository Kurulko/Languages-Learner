using System.Security.Claims;
using WebApi.Models.Database;

namespace WebApi.Services.Account;

public interface IJwtService
{
    (string token, int expirationDays) GenerateJwtToken(User user, IEnumerable<string> roles);
    ClaimsPrincipal GetPrincipalFromToken(string token);
}

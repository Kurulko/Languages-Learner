using System.Security.Claims;
using WebApi.Models.Database;

namespace WebApi.Services.UserServices;

public interface IBaseUserService : IDbModelService<User, string>
{
    Task<User> CreateUser();
    Task<string?> GetUserIdByUserNameAsync(string userName);
    Task<User?> GetUserByClaimsAsync(ClaimsPrincipal claims);
    Task<User?> GetUserByNameAsync(string name);
}

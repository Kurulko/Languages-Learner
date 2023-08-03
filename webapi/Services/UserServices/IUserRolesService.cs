using WebApi.Models.Database;
using WebApi.Models.Helpers;

namespace WebApi.Services.UserServices;

public interface IUserRolesService
{
    Task<IEnumerable<string>> GetRolesAsync(string? userId);
    Task<IEnumerable<User>> GetUsersByRoleAsync(string roleName);
    Task AddRoleToUserAsync(ModelWithUserId<string> model);
    Task DeleteRoleFromUserAsync(ModelWithUserId<string> model);
}

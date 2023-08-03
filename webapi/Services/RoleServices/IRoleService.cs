using WebApi.Models.Database;

namespace WebApi.Services.RoleServices;

public interface IRoleService : IDbModelService<Role, string>
{
    Task<Role?> GetRoleByNameAsync(string name);
    Task<Role> CreateRole();
}
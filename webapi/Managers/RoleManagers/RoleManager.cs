using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models.Database;
using WebApi.Services.RoleServices;

namespace WebApi.Managers.RoleManagers;

public class RoleManager : IRoleService
{
    readonly RoleManager<Role> roleManager;
    readonly LearnerContext db;
    public RoleManager(RoleManager<Role> roleManager, LearnerContext db)
        => (this.roleManager, this.db) = (roleManager, db);

    public async Task<Role> AddModelAsync(Role model)
    {
        await db.Roles.AddAsync(model);
        db.SaveChanges();
        return model;
    }

    public Task<Role> CreateRole()
        => Task.FromResult(new Role());

    public async Task DeleteModelAsync(string key)
    {
        Role? role = await GetModelByIdAsync(key);
        if (role is not null)
            await roleManager.DeleteAsync(role);
    }

    public async Task<IEnumerable<Role>> GetAllModelsAsync()
        => await db.Roles.ToListAsync();

    public async Task<Role?> GetModelByIdAsync(string key)
        => await db.Roles.FirstOrDefaultAsync(u => u.Id == key);

    public async Task<Role?> GetRoleByNameAsync(string name)
        => await roleManager.FindByNameAsync(name);

    public async Task UpdateModelAsync(Role model)
        => await roleManager.UpdateAsync(model);
}
using Microsoft.AspNetCore.Identity;
using System.Data;
using WebApi.Context;
using WebApi.Models.Database;
using WebApi.Models.Helpers;
using WebApi.Services.UserServices;

namespace WebApi.Managers.UserManagers;

public class UserRolesManager : BaseUserManager, IUserRolesService
{
    public UserRolesManager(UserManager<User> userManager, LearnerContext db, IHttpContextAccessor httpContextAccessor) : base(userManager, db, httpContextAccessor)
    { }

    public async Task AddRoleToUserAsync(ModelWithUserId<string> model)
    {
        string roleName = model.Model;
        User user = await GetUserByIdAsync(model.UserId);
        IdentityResult res = await userManager.AddToRoleAsync(user, roleName);
        if (!res.Succeeded)
            throw new Exception(string.Join("; ", res.Errors.Select(e => e.Description)));
    }

    public async Task AddUserPasswordAsync(ModelWithUserId<string> model)
    {
        string newPassword = model.Model;
        User user = await GetUserByIdAsync(model.UserId);
        IdentityResult res = await userManager.AddPasswordAsync(user, newPassword);
        if (!res.Succeeded)
            throw new Exception(string.Join("; ", res.Errors.Select(e => e.Description)));
    }
    public async Task DeleteRoleFromUserAsync(ModelWithUserId<string> model)
    {
        User user = await GetUserByIdAsync(model.UserId);
        IdentityResult res = await userManager.RemoveFromRoleAsync(user, model.Model);
        if (!res.Succeeded)
            throw new Exception(string.Join("; ", res.Errors.Select(e => e.Description)));
    }

    public async Task<IEnumerable<string>> GetRolesAsync(string? userId)
    {
        User user = await (userId is null ? GetUsedUserAsync() : GetUserByIdAsync(userId));
        return await userManager.GetRolesAsync(user!);
    }

    public async Task<IEnumerable<User>> GetUsersByRoleAsync(string roleName)
    {
        IList<User> usersByRole = new List<User>();

        var allUser = await GetAllModelsAsync();

        foreach (User user in allUser)
        {
            var userRoles = await GetRolesAsync(user.Id);
            if (userRoles.Contains(roleName))
                usersByRole.Add(user);
        }

        return usersByRole;
    }
}

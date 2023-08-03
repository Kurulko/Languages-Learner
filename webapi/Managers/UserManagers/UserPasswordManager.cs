using Microsoft.AspNetCore.Identity;
using WebApi.Context;
using WebApi.Models.Account;
using WebApi.Models.Database;
using WebApi.Models.Helpers;
using WebApi.Services.UserServices;

namespace WebApi.Managers.UserManagers;

public class UserPasswordManager : BaseUserManager, IUserPasswordService
{
    public UserPasswordManager(UserManager<User> userManager, LearnerContext db, IHttpContextAccessor httpContextAccessor) : base(userManager, db, httpContextAccessor)
    { }

    public async Task AddUserPasswordAsync(ModelWithUserId<string> model)
    {
        string newPassword = model.Model;
        User user = await GetUserByIdAsync(model.UserId);
        IdentityResult res = await userManager.AddPasswordAsync(user, newPassword);
        if (!res.Succeeded)
            throw new Exception(string.Join("; ", res.Errors.Select(e => e.Description)));
    }

    public async Task ChangeUserPasswordAsync(ChangePassword model)
    {
        ChangePassword password = model;
        User user = await GetUsedUserAsync();
        IdentityResult res = await userManager.ChangePasswordAsync(user, password.OldPassword!, password.NewPassword);
        if (!res.Succeeded)
            throw new Exception(string.Join("; ", res.Errors.Select(e => e.Description)));
    }

    public async Task<bool> HasUserPasswordAsync(string userId)
    {
        User user = await GetUserByIdAsync(userId);
        return await userManager.HasPasswordAsync(user);
    }
}

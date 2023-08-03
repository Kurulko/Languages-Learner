using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models.Database;
using WebApi.Models.Database.Learner;
using WebApi.Services.UserServices;

namespace WebApi.Managers.UserManagers;

public class UsedUserManager : BaseUserManager, IUsedUserService
{
    public UsedUserManager(UserManager<User> userManager, LearnerContext db, IHttpContextAccessor httpContextAccessor) : base(userManager, db, httpContextAccessor)
    { }

    public async Task<string?> GetCurrentUserNameAsync()
        => (await GetUsedUserAsync())?.UserName;

    public async Task ChangeUsedUserIdAsync(string usedUserId)
    {
        User? user = await GetCurrentUserAsync();
        if (user is not null && !string.IsNullOrEmpty(usedUserId))
        {
            user.UsedUserId = usedUserId;
            await UpdateModelAsync(user);
        }
    }

    public async Task DropUsedUserIdAsync()
    {
        User? user = await GetCurrentUserAsync();
        if (user is not null)
        {
            user.UsedUserId = null;
            await UpdateModelAsync(user);
        }
    }

    public async Task<string> GetUsedUserChatGPTTokenAsync()
        => (await GetUsedUserAsync()).ChatGPTToken;

    public new async Task<User> GetUsedUserAsync()
        => await base.GetUsedUserAsync();

    public new async Task<string> GetUsedUserIdAsync()
        => await base.GetUsedUserIdAsync();

    public async Task<bool> IsImpersonating()
        => !string.IsNullOrEmpty((await GetCurrentUserAsync())?.UsedUserId);
}

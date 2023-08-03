using WebApi.Models.Database;
using WebApi.Models.Database.Learner;

namespace WebApi.Services.UserServices;

public interface IUsedUserService
{
    Task<bool> IsImpersonating();

    Task<string?> GetCurrentUserNameAsync();

    Task<User> GetUsedUserAsync();
    Task<string> GetUsedUserIdAsync();
    Task<string> GetUsedUserChatGPTTokenAsync();

    Task ChangeUsedUserIdAsync(string usedUserId);
    Task DropUsedUserIdAsync();
}

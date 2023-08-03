using WebApi.Models.Account;
using WebApi.Models.Helpers;

namespace WebApi.Services.UserServices;

public interface IUserPasswordService
{
    Task ChangeUserPasswordAsync(ChangePassword model);
    Task AddUserPasswordAsync(ModelWithUserId<string> model);
    Task<bool> HasUserPasswordAsync(string userId);
}

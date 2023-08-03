using WebApi.Models.Account;

namespace WebApi.Services.Account;

public interface IAccountService
{
    Task<IEnumerable<string>> LoginUserAsync(LoginModel model);
    Task<IEnumerable<string>> RegisterUserAsync(RegisterModel model);
    Task LogoutUserAsync();
}
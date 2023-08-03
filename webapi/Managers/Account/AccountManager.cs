using Microsoft.AspNetCore.Identity;
using WebApi.Managers.UserManagers;
using WebApi.Models.Account;
using WebApi.Models.Database;
using WebApi.Services.Account;
namespace WebApi.Managers.Account;

public class AccountManager : IAccountService
{
    readonly SignInManager<User> signInManager;
    readonly UserManager<User> userManager;

    public AccountManager(SignInManager<User> signInManager, UserManager<User> userManager)
        => (this.signInManager, this.userManager) = (signInManager, userManager);

    public async Task<IEnumerable<string>> LoginUserAsync(LoginModel login)
    {
        var res = await signInManager.PasswordSignInAsync(login.Name, login.Password, login.RememberMe, false);

        if (!res.Succeeded)
            throw new Exception("Password or/and login invalid");

        User user = (await userManager.FindByNameAsync(login.Name))!;
        return await userManager.GetRolesAsync(user);
    }

    public async Task<IEnumerable<string>> RegisterUserAsync(RegisterModel register)
    {
        User user = (User)register;
        IdentityResult result = await userManager.CreateAsync(user, register.Password);
        if (result.Succeeded)
        {
            user.Registered = DateTime.Now;
            await signInManager.SignInAsync(user, register.RememberMe);
            string userRole = Roles.User;
            await userManager.AddToRolesAsync(user, new List<string>() { userRole });
            return new string[] { userRole };
        }
        else
            throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
    }

    public async Task LogoutUserAsync()
        => await signInManager.SignOutAsync();
}

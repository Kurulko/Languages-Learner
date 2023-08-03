using Microsoft.AspNetCore.Identity;
using WebApi.Models.Database;
using WebApi.Settings;

namespace WebApi.Initializers;

public class UsersInitializer
{
    public static async Task AdminInitializeAsync(UserManager<User> userManager, ChatGPTSettings chatGPTSettings, string name, string password)
    {
        if (await userManager.FindByNameAsync(name) is null)
        {
            User admin = new() { UserName = name, Registered = DateTime.Now, ChatGPTToken = chatGPTSettings.DefaultChatGPTToken };
            IdentityResult result = await userManager.CreateAsync(admin, password);
            if (result.Succeeded)
                await userManager.AddToRoleAsync(admin, Roles.Admin);
        }
    }
}
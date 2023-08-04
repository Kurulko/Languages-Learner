using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using WebApi.Context;
using WebApi.Models.Database;
using WebApi.Services;
using WebApi.Services.UserServices;

namespace WebApi.Managers.UserManagers;

public class BaseUserManager : IBaseUserService
{
    protected readonly IHttpContextAccessor httpContextAccessor;
    protected readonly UserManager<User> userManager;
    protected readonly LearnerContext db;
    public BaseUserManager(UserManager<User> userManager, LearnerContext db, IHttpContextAccessor httpContextAccessor)
    {
        this.userManager = userManager;
        this.httpContextAccessor = httpContextAccessor;
        this.db = db;

        getModels = db.Users;
    }

    protected IQueryable<User> getModels;

    public Task<User> CreateUser()
        => Task.FromResult(new User());

    public async Task<User> AddModelAsync(User model)
    {
        User? existingUser = await GetModelByIdAsync(model.Id);
        if (existingUser is null)
        {
            await userManager.CreateAsync(model);
            return model;
        }
        return existingUser;
    }

    public async Task UpdateModelAsync(User model)
    {
        string updateSql = "UPDATE AspNetUsers " +
            "SET UserName = @UserName, NormalizedUserName = @NormalizedUserName " +
            "WHERE Id = @Id";
        await db.Database.ExecuteSqlRawAsync(updateSql, 
            new SqlParameter("@UserName", model.UserName),
            new SqlParameter("@NormalizedUserName", model.UserName.ToLower()),
            new SqlParameter("@Id", model.Id));
    }

    public async Task<IEnumerable<User>> GetAllModelsAsync()
        => await getModels.AsNoTracking().ToListAsync();

    public async Task<User?> GetModelByIdAsync(string key)
        => await getModels.AsNoTracking().SingleOrDefaultAsync(u => u.Id == key);

    public async Task DeleteModelAsync(string key)
    {
        User? user = await GetModelByIdAsync(key);
        if (user is not null)
            await userManager.DeleteAsync(user);
    }


    public async Task<User?> GetUserByClaimsAsync(ClaimsPrincipal claims)
        => await GetUserByNameAsync(claims.Identity!.Name!);

    public async Task<string?> GetUserIdByUserNameAsync(string userName)
        => (await GetUserByNameAsync(userName))?.Id;

    public async Task<User?> GetUserByNameAsync(string name)
        => await getModels.SingleOrDefaultAsync(u => u.UserName == name);

    protected async Task<User> GetUserByIdAsync(string userId)
    {
        User? user = await GetModelByIdAsync(userId);
        if (user is null)
            throw new ArgumentException($"The user with id '{userId}' doesn't exist");
        return user!;
    }

    protected async Task<User> GetUsedUserAsync()
    {
        string usedUserId = await GetUsedUserIdAsync();
        User usedUser = (await GetModelByIdAsync(usedUserId))!;
        return usedUser;
    }

    protected async Task<string> GetUsedUserIdAsync()
    {
        User currentUser = await GetCurrentUserAsync()!;
        return currentUser.UsedUserId ?? currentUser.Id;
    }

    protected async Task<User> GetCurrentUserAsync()
    {
        var claims = httpContextAccessor.HttpContext!.User;
        return (await GetUserByClaimsAsync(claims))!;
    }
}

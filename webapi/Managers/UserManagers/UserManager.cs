using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Security.Claims;
using WebApi.Context;
using WebApi.Enums;
using WebApi.Models.Account;
using WebApi.Models.Database;
using WebApi.Models.Database.Learner;
using WebApi.Models.Database.Learner.ByLanguages;
using WebApi.Models.Helpers;
using WebApi.Services.UserServices;

namespace WebApi.Managers.UserManagers;

public class UserManager : IUserService
{
    readonly IBaseUserService baseUserService;
    readonly IUserRolesService userRolesService;
    readonly IUserPasswordService userPasswordService;
    readonly IUsedUserService usedUserService;
    readonly IUserModelsService userModelsService;
    public UserManager(IBaseUserService baseUserService, IUserRolesService userRolesService, IUserPasswordService userPasswordService, IUsedUserService usedUserService, IUserModelsService userModelsService)
    {
        this.baseUserService = baseUserService;
        this.userRolesService = userRolesService;
        this.userPasswordService = userPasswordService;
        this.usedUserService = usedUserService;
        this.userModelsService = userModelsService;
    }

    public async Task<User> AddModelAsync(User model)
        => await baseUserService.AddModelAsync(model);

    public async Task AddRoleToUserAsync(ModelWithUserId<string> model)
        => await userRolesService.AddRoleToUserAsync(model);

    public async Task AddUserPasswordAsync(ModelWithUserId<string> model)
        => await userPasswordService.AddUserPasswordAsync(model);

    public async Task ChangeUsedUserIdAsync(string usedUserId)
        => await usedUserService.ChangeUsedUserIdAsync( usedUserId);

    public async Task ChangeUserPasswordAsync(ChangePassword model)
        => await userPasswordService.ChangeUserPasswordAsync(model);

    public async Task<User> CreateUser()
        => await baseUserService.CreateUser();

    public async Task DeleteModelAsync(string key)
        => await baseUserService.DeleteModelAsync(key);

    public async Task DeleteRoleFromUserAsync(ModelWithUserId<string> model)
        => await userRolesService.DeleteRoleFromUserAsync(model);

    public async Task DropUsedUserIdAsync()
        => await usedUserService.DropUsedUserIdAsync();

    public async Task<IEnumerable<User>> GetAllModelsAsync()
        => await baseUserService.GetAllModelsAsync();

    public async Task<string?> GetCurrentUserNameAsync()
        => await usedUserService.GetCurrentUserNameAsync();

    public async Task<User?> GetModelByIdAsync(string key)
        => await baseUserService.GetModelByIdAsync(key);

    public async Task<IEnumerable<string>> GetRolesAsync(string? userId)
        => await userRolesService.GetRolesAsync(userId);

    public async Task<User> GetUsedUserAsync()
        => await usedUserService.GetUsedUserAsync();

    public async Task<string> GetUsedUserChatGPTTokenAsync()
        => await usedUserService.GetUsedUserChatGPTTokenAsync();

    public async Task<string> GetUsedUserIdAsync()
        => await usedUserService.GetUsedUserIdAsync();

    public async Task<User?> GetUserByClaimsAsync(ClaimsPrincipal claims)
        => await baseUserService.GetUserByClaimsAsync(claims);

    public async Task<User?> GetUserByNameAsync(string name)
        => await baseUserService.GetUserByNameAsync(name);

    public async Task<string?> GetUserIdByUserNameAsync(string userName)
        => await baseUserService.GetUserIdByUserNameAsync(userName);

    public async Task<IndexViewModel<IdiomByLanguage>> GetUserIdiomsByLanguageNameAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string languageName, string? userId = null)
        => await userModelsService.GetUserIdiomsByLanguageNameAsync(attribute, orderBy, pageSize, pageNumber, languageName, userId);

    public async Task<IndexViewModel<IdiomByLanguage>> GetUserIdiomsByLanguagesAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string? userId = null)
        => await userModelsService.GetUserIdiomsByLanguagesAsync(attribute, orderBy, pageSize, pageNumber, userId);

    public async Task<IndexViewModel<RuleByLanguage>> GetUserRulesByLanguageNameAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string languageName, string? userId = null)
        => await userModelsService.GetUserRulesByLanguageNameAsync(attribute, orderBy, pageSize, pageNumber, languageName, userId);

    public async Task<IndexViewModel<RuleByLanguage>> GetUserRulesByLanguagesAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string? userId = null)
        => await userModelsService.GetUserRulesByLanguagesAsync(attribute, orderBy, pageSize, pageNumber, userId);

    public async Task<IndexViewModel<SentenceByLanguage>> GetUserSentencesByLanguageNameAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string languageName, string? userId = null)
        => await userModelsService.GetUserSentencesByLanguageNameAsync(attribute, orderBy, pageSize, pageNumber, languageName, userId);

    public async Task<IndexViewModel<SentenceByLanguage>> GetUserSentencesByLanguagesAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string? userId = null)
        => await userModelsService.GetUserSentencesByLanguagesAsync(attribute, orderBy, pageSize, pageNumber, userId);

    public async Task<IndexViewModel<WordByLanguage>> GetUserWordsByLanguageNameAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string languageName, string? userId = null)
        => await userModelsService.GetUserWordsByLanguageNameAsync(attribute, orderBy, pageSize, pageNumber, languageName, userId);

    public async Task<IndexViewModel<WordByLanguage>> GetUserWordsByLanguagesAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string? userId = null)
        => await userModelsService.GetUserWordsByLanguagesAsync(attribute, orderBy, pageSize, pageNumber, userId);

    public async Task<Language?> GetUserCurrentLanguageAsync(string? userId = null)
        => await userModelsService.GetUserCurrentLanguageAsync();

    public async Task ChangeCurrentLanguage(Language language, string? userId = null)
        => await userModelsService.ChangeCurrentLanguage(language);

    public async Task<bool> HasUserPasswordAsync(string userId)
        => await userPasswordService.HasUserPasswordAsync(userId);

    public async Task<bool> IsImpersonating()
        => await usedUserService.IsImpersonating();

    public async Task UpdateModelAsync(User model)
        => await baseUserService.UpdateModelAsync(model);

    public async Task<IEnumerable<User>> GetUsersByRoleAsync(string roleName)
        => await userRolesService.GetUsersByRoleAsync(roleName);
}

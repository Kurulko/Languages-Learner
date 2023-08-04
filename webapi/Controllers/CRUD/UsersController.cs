using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Security.Claims;
using WebApi.Enums;
using WebApi.Extensions;
using WebApi.Models.Account;
using WebApi.Models.Database;
using WebApi.Models.Database.Learner;
using WebApi.Models.Database.Learner.ByLanguages;
using WebApi.Models.Helpers;
using WebApi.Services.Account;
using WebApi.Services.Learner.ByLanguage;
using WebApi.Services.UserServices;

namespace WebApi.Controllers.CRUD;

public class UsersController : AdminDbModelsController<User, string>
{
    readonly IUserService userService;
    readonly IJwtService jwtService;
    public UsersController(IUserService service, IJwtService jwtService) : base(service)
        => (this.userService, this.jwtService) = (service, jwtService);

    #region User

    [AllowAnonymous]
    public override async Task<IActionResult> UpdateModelAsync(User model)
        => await ReturnOkIfEverithingIsGood(async () =>
        {
            string userId = model.Id;
            await CheckAccessForUser(userId);
            await service.UpdateModelAsync(model);
            IEnumerable<string> roles = await userService.GetRolesAsync(userId);
            var tokenInfo = jwtService.GenerateJwtToken(model, roles);
            return new { tokenInfo.token, tokenInfo.expirationDays };
        });

    [HttpGet("user-by-default")]
    public async Task<User> CreateUser()
        => await userService.CreateUser();

    [AllowAnonymous]
    [HttpGet("userid-by-name/{userName}")]
    public async Task<string?> GetUserIdByUserNameAsync(string userName)
        => await userService.GetUserIdByUserNameAsync(userName);

    [AllowAnonymous]
    [HttpGet("current-username")]
    public async Task<string?> GetCurrentUserNameAsync()
        => await CheckAccess(userService.GetCurrentUserNameAsync);

    [AllowAnonymous]
    [HttpGet("is-impersonating")]
    public async Task<bool> IsImpersonating()
        => await CheckAccess(userService.IsImpersonating);

    [AllowAnonymous]
    public override async Task<User?> GetModelByIdAsync(string key)
        => await CheckAccessForUser(key, () => base.GetModelByIdAsync(key));

    [AllowAnonymous]
    [HttpGet("current")]
    public virtual async Task<User?> GetUserByClaimsAsync()
        => await CheckAccess(() => userService.GetUserByClaimsAsync(User));


    [HttpGet("name")]
    public virtual async Task<User?> GetUserByNameAsync(string name)
        => await userService.GetUserByNameAsync(name);

    #endregion

    #region UsedUser

    [HttpGet("usedUser")]
    public virtual async Task<User> GetUsedUserAsync()
        => await userService.GetUsedUserAsync();

    [HttpPut("change-used-userId")]
    public async Task ChangeUsedUserIdAsync([FromForm]string usedUserId)
        => await ReturnOkIfEverithingIsGood(async () => await userService.ChangeUsedUserIdAsync(usedUserId));

    [HttpDelete("drop-used-userId")]
    public async Task DropUsedUserIdAsync()
        => await ReturnOkIfEverithingIsGood(userService.DropUsedUserIdAsync);

    #endregion

    const string pathToUserCurrentLanguage = "user-current-language", pathToUserSentences = "user-languages-sentences", pathToUserWords = "user-languages-words"
    , pathToUserRules = "user-languages-rules", pathToUserIdioms = "user-languages-idioms";

    const string pathToUnnecessaryUserId = "{userId?}";

    #region UserModels

    [AllowAnonymous]
    [HttpGet($"{pathToUserCurrentLanguage}/{pathToUnnecessaryUserId}")]
    public async Task<Language?> GetUserCurrentLanguageAsync(string? userId = null)
        => await CheckAccess(() => userService.GetUserCurrentLanguageAsync(userId));

    [AllowAnonymous]
    [HttpPut($"{pathToUserCurrentLanguage}/{pathToUnnecessaryUserId}")]
    public async Task<IActionResult> ChangeCurrentLanguage([FromBody] Language language, [FromRoute] string? userId = null)
        => await ReturnOkIfEverithingIsGood(async () => {
            CheckAccess();
            await userService.ChangeCurrentLanguage(language, userId);
        });

    [AllowAnonymous]
    [HttpGet($"{pathToUserSentences}/{pathToUnnecessaryUserId}")]
    public virtual async Task<IndexViewModel<SentenceByLanguage>?> GetUserSentencesByLanguagesAsync([FromQuery] string? attribute, [FromQuery] string? orderBy, [FromQuery] int? pageNumber, [FromQuery] int? pageSize, string? userId = null)
        => await CheckAccess(() => userService.GetUserSentencesByLanguagesAsync(attribute, orderBy?.ParseToOrderBy(), pageSize, pageNumber, userId));


    [AllowAnonymous]
    [HttpGet($"{pathToUserWords}/{pathToUnnecessaryUserId}")]
    public virtual async Task<IndexViewModel<WordByLanguage>> GetUserWordsByLanguagesAsync([FromQuery] string? attribute, [FromQuery] string? orderBy, [FromQuery] int? pageNumber, [FromQuery] int? pageSize, string? userId = null)
        => await CheckAccess(() => userService.GetUserWordsByLanguagesAsync(attribute, orderBy?.ParseToOrderBy(), pageSize, pageNumber, userId));

    [AllowAnonymous]
    [HttpGet($"{pathToUserRules}/{pathToUnnecessaryUserId}")]
    public virtual async Task<IndexViewModel<RuleByLanguage>> GetUserRulesByLanguagesAsync([FromQuery] string? attribute, [FromQuery] string? orderBy, [FromQuery] int? pageNumber, [FromQuery] int? pageSize, string? userId = null)
        => await CheckAccess(() => userService.GetUserRulesByLanguagesAsync(attribute, orderBy?.ParseToOrderBy(), pageSize, pageNumber, userId));

    [AllowAnonymous]
    [HttpGet($"{pathToUserIdioms}/{pathToUnnecessaryUserId}")]
    public virtual async Task<IndexViewModel<IdiomByLanguage>> GetUserIdiomsByLanguagesAsync([FromQuery] string? attribute, [FromQuery] string? orderBy, [FromQuery] int? pageNumber, [FromQuery] int? pageSize, string? userId = null)
        => await CheckAccess(() => userService.GetUserIdiomsByLanguagesAsync(attribute, orderBy?.ParseToOrderBy(), pageSize, pageNumber, userId));

    #endregion

    #region UserModelsByLanguageName

    const string pathTolanguageName = "by-name/{languageName}";

    [AllowAnonymous]
    [HttpGet($"{pathToUserSentences}/{pathTolanguageName}/{pathToUnnecessaryUserId}")]
    public virtual async Task<IndexViewModel<SentenceByLanguage>?> GetUserSentencesByLanguageNameAsync([FromQuery] string? attribute, [FromQuery] string? orderBy, [FromQuery] int? pageNumber, [FromQuery] int? pageSize, string languageName, string? userId = null)
        => await CheckAccess(() => userService.GetUserSentencesByLanguageNameAsync(attribute, orderBy?.ParseToOrderBy(), pageSize, pageNumber, languageName, userId));

    [AllowAnonymous]
    [HttpGet($"{pathToUserWords}/{pathTolanguageName}/{pathToUnnecessaryUserId}")]
    public virtual async Task<IndexViewModel<WordByLanguage>> GetUserWordsByLanguageNameAsync([FromQuery] string? attribute, [FromQuery] string? orderBy, [FromQuery] int? pageNumber, [FromQuery] int? pageSize, string languageName, string? userId = null)
        => await CheckAccess(() => userService.GetUserWordsByLanguageNameAsync(attribute, orderBy?.ParseToOrderBy(), pageSize, pageNumber, languageName, userId));

    [AllowAnonymous]
    [HttpGet($"{pathToUserRules}/{pathTolanguageName}/{pathToUnnecessaryUserId}")]
    public virtual async Task<IndexViewModel<RuleByLanguage>> GetUserRulesByLanguageNameAsync([FromQuery] string? attribute, [FromQuery] string? orderBy, [FromQuery] int? pageNumber, [FromQuery] int? pageSize, string languageName, string? userId = null)
        => await CheckAccess(() => userService.GetUserRulesByLanguageNameAsync(attribute, orderBy?.ParseToOrderBy(), pageSize, pageNumber, languageName, userId));

    [AllowAnonymous]
    [HttpGet($"{pathToUserIdioms}/{pathTolanguageName}/{pathToUnnecessaryUserId}")]
    public virtual async Task<IndexViewModel<IdiomByLanguage>> GetUserIdiomsByLanguageNameAsync([FromQuery] string? attribute, [FromQuery] string? orderBy, [FromQuery] int? pageNumber, [FromQuery] int? pageSize, string languageName, string? userId = null)
        => await CheckAccess(() => userService.GetUserIdiomsByLanguageNameAsync(attribute, orderBy?.ParseToOrderBy(), pageSize, pageNumber, languageName, userId));

    #endregion

    #region Password

    [AllowAnonymous]
    [HttpGet("{userId}/password")]
    public async Task<bool> HasPassword(string userId)
        => await CheckAccessForUser(userId, () => userService.HasUserPasswordAsync(userId));

    [AllowAnonymous]
    [HttpPut("password")]
    public async Task<IActionResult> ChangePassword(ChangePassword model)
        => await ReturnOkIfEverithingIsGood(async () =>
        {
            CheckAccess();
            await userService.ChangeUserPasswordAsync(model);
        });

    [HttpPost("password")]
    public async Task<IActionResult> CreatePassword(ModelWithUserId<string> model)
        => await ReturnOkIfEverithingIsGood(async () => await userService.AddUserPasswordAsync(model));

    #endregion

    #region Roles

    [AllowAnonymous]
    [HttpGet("user-roles/{userId?}")]
    public async Task<IEnumerable<string>> GetRoles(string? userId)
        => await (userId is null ?
         CheckAccess(() => userService.GetRolesAsync(userId)) :
         CheckAccessForUser(userId, () => userService.GetRolesAsync(userId)));

    [HttpPost("{userId}/role")]
    public async Task<IActionResult> AddRole(string userId, [FromBody] string roleName)
        => await ReturnOkIfEverithingIsGood(async () => await userService.AddRoleToUserAsync(new(userId, roleName)));

    [HttpDelete("{userId}/{roleName}")]
    public async Task<IActionResult> DeleteRole(string userId, string roleName)
        => await ReturnOkIfEverithingIsGood(async () => await userService.DeleteRoleFromUserAsync(new(userId, roleName)));

    [HttpGet("users-by-role/{roleName}")]
    public async Task<IEnumerable<User>> GetUsersByRoleAsync(string roleName)
        => await userService.GetUsersByRoleAsync(roleName);

    #endregion

    async Task CheckAccessForUser(string userId)
    {
        string? userName = User.Identity?.Name;
        string? _userId = await userService.GetUserIdByUserNameAsync(userName!);
        if (!(User.IsInRole(Roles.Admin) || _userId == userId))
            AccessDenied();
    }
    async Task<T> CheckAccessForUser<T>(string userId, Func<Task<T>> actionAsync)
    {
        await CheckAccessForUser(userId);
        return await actionAsync();
    }

    void CheckAccess()
    {
        if (!User.Identity?.IsAuthenticated ?? false)
            AccessDenied();
    }
    async Task<T> CheckAccess<T>(Func<Task<T>> actionAsync)
    {
        CheckAccess();
        return await actionAsync();
    }

    void AccessDenied()
        => throw new UnauthorizedAccessException("Access to this source is denied!");
}
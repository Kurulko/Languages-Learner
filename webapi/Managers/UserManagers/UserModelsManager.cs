using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Context;
using WebApi.Models.Database;
using WebApi.Models.Database.Learner.ByLanguages;
using WebApi.Models.Database.Learner;
using WebApi.Services.UserServices;
using WebApi.Enums;
using WebApi.Extensions;
using WebApi.Models.Helpers;

namespace WebApi.Managers.UserManagers;

public class UserModelsManager : BaseUserManager, IUserModelsService
{
    public UserModelsManager(UserManager<User> userManager, LearnerContext db, IHttpContextAccessor httpContextAccessor) : base(userManager, db, httpContextAccessor)
    { }


    IndexViewModel<T> GetFilteredData<T>(IEnumerable<T> models, string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber)
        where T : ILearnerModel
    {
        int countOfAllModels = models.Count();

        attribute = attribute ?? nameof(ILearnerModel.Id);
        orderBy = orderBy ?? OrderBy.Ascending;
        pageNumber = pageNumber ?? 1;
        pageSize = pageSize ?? countOfAllModels;

        return models.OrderBy(attribute, orderBy.Value).Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value).ToIndexViewModel(countOfAllModels, pageSize, pageNumber);
    }

    async Task<User> GetUsedUserIfUserIdIsNull(string? userId)
        => await (userId is null ? GetUsedUserAsync() : GetUserByIdAsync(userId));

    async Task<IndexViewModel<T>> GetUserModelsByLanguageAsync<T>(Expression<Func<User, IEnumerable<T>>> include, string? userId, string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber)
        where T : ByLanguageModel
    {
        getModels = getModels.Include(include).ThenInclude(model => model.Language!);
        User user = await GetUsedUserIfUserIdIsNull(userId);
        return GetFilteredData(include.Compile()(user), attribute, orderBy, pageSize, pageNumber);
    }


    public async Task<IndexViewModel<SentenceByLanguage>> GetUserSentencesByLanguagesAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string? userId = null)
       => await GetUserModelsByLanguageAsync(u => u.SentencesByLanguages!, userId, attribute, orderBy, pageSize, pageNumber);

    public async Task<IndexViewModel<WordByLanguage>> GetUserWordsByLanguagesAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string? userId = null)
       => await GetUserModelsByLanguageAsync(u => u.WordsByLanguages!, userId, attribute, orderBy, pageSize, pageNumber);

    public async Task<IndexViewModel<RuleByLanguage>> GetUserRulesByLanguagesAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string? userId = null)
       => await GetUserModelsByLanguageAsync(u => u.RulesByLanguages!, userId, attribute, orderBy, pageSize, pageNumber);

    public async Task<IndexViewModel<IdiomByLanguage>> GetUserIdiomsByLanguagesAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string? userId = null)
       => await GetUserModelsByLanguageAsync(u => u.IdiomsByLanguages!, userId, attribute, orderBy, pageSize, pageNumber);


    async Task<IndexViewModel<T>> GetIndexViewUserByLanguageModelsByLanguageNameAsync<T>(Expression<Func<User, IEnumerable<T>>> expression, string languageName, string? userId, string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber)
        where T : ByLanguageModel
    {
        var indexViewModel = await GetUserModelsByLanguageAsync(expression, userId, attribute, orderBy, pageSize, pageNumber);
        var models = indexViewModel.Models.Where(m => m.Language!.Name == languageName);

        return models.ToIndexViewModel(indexViewModel.Models.Count(), pageSize, pageNumber);
    }

    public async Task<IndexViewModel<SentenceByLanguage>> GetUserSentencesByLanguageNameAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string languageName, string? userId = null)
       => await GetIndexViewUserByLanguageModelsByLanguageNameAsync(u => u.SentencesByLanguages!, languageName, userId, attribute, orderBy, pageSize, pageNumber);

    public async Task<IndexViewModel<WordByLanguage>> GetUserWordsByLanguageNameAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string languageName, string? userId = null)
       => await GetIndexViewUserByLanguageModelsByLanguageNameAsync(u => u.WordsByLanguages!, languageName, userId, attribute, orderBy, pageSize, pageNumber);

    public async Task<IndexViewModel<RuleByLanguage>> GetUserRulesByLanguageNameAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string languageName, string? userId = null)
       => await GetIndexViewUserByLanguageModelsByLanguageNameAsync(u => u.RulesByLanguages!, languageName, userId, attribute, orderBy, pageSize, pageNumber);

    public async Task<IndexViewModel<IdiomByLanguage>> GetUserIdiomsByLanguageNameAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string languageName, string? userId = null)
       => await GetIndexViewUserByLanguageModelsByLanguageNameAsync(u => u.IdiomsByLanguages!, languageName, userId, attribute, orderBy, pageSize, pageNumber);

    public async Task<Language?> GetUserCurrentLanguageAsync(string? userId = null)
    {
        getModels = getModels.Include(u => u.CurrentLanguage);
        return (await GetUsedUserIfUserIdIsNull(userId)).CurrentLanguage;
    }

    public async Task ChangeCurrentLanguage(Language language, string? userId = null)
    {
        User user = await GetUsedUserIfUserIdIsNull(userId);
        if (user is not null)
        {
            user.CurrentLanguage = language;
            await UpdateModelAsync(user);
        }
    }
}

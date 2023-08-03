using WebApi.Models.Database;
using WebApi.Models.Database.Learner.ByLanguages;
using WebApi.Models.Database.Learner;
using WebApi.Enums;
using WebApi.Models.Helpers;

namespace WebApi.Services.UserServices;

public interface IUserModelsService
{
    Task<IndexViewModel<SentenceByLanguage>> GetUserSentencesByLanguagesAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string? userId = null);
    Task<IndexViewModel<WordByLanguage>> GetUserWordsByLanguagesAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string? userId = null);
    Task<IndexViewModel<RuleByLanguage>> GetUserRulesByLanguagesAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string? userId = null);
    Task<IndexViewModel<IdiomByLanguage>> GetUserIdiomsByLanguagesAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string? userId = null);

    Task<IndexViewModel<SentenceByLanguage>> GetUserSentencesByLanguageNameAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string languageName, string? userId = null);
    Task<IndexViewModel<WordByLanguage>> GetUserWordsByLanguageNameAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string languageName, string? userId = null);
    Task<IndexViewModel<RuleByLanguage>> GetUserRulesByLanguageNameAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string languageName, string? userId = null);
    Task<IndexViewModel<IdiomByLanguage>> GetUserIdiomsByLanguageNameAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string languageName, string? userId = null);

    Task<Language?> GetUserCurrentLanguageAsync(string? userId = null);
    Task ChangeCurrentLanguage(Language language, string? userId = null);
}

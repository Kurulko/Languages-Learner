using OpenAI_API.Chat;
using OpenAI_API;
using System;
using WebApi.Extensions;
using WebApi.Models.Database.Learner.ByLanguages;
using WebApi.Services.Learner;
using WebApi.Services.AI;
using WebApi.Services.UserServices;

namespace WebApi.Managers.AI;

public class IdiomsGeneratorManager : GeneratorManager<IdiomByLanguage>, IIdiomsGeneratorService 
{
    public IdiomsGeneratorManager(IUserService userService, ILanguageService languageService) : base(userService, languageService, "idioms") { }

    protected override IEnumerable<IdiomByLanguage> FromResponseStringToItemsAsync(string response, long languageId)
        => response.ParseToIdiomsByLanguageFromNumeredIdiomsStr(languageId);

    public async Task<IEnumerable<IdiomByLanguage>> GenerateIdiomsAsync(string languageName, int? count = null)
        => await GetResponseInItemsAsync(languageName, count);

    public async Task<IEnumerable<IdiomByLanguage>> GenerateIdiomsAsync(long languageId, int? count = null)
        => await GetResponseInItemsAsync(languageId, count);

    public async Task<IEnumerable<IdiomByLanguage>> GenerateIdiomsWithWordAsync(string languageName, string word, int? count = null)
        => await GetResponseInItemsWithWordsAsync(languageName, count, word);

    public async Task<IEnumerable<IdiomByLanguage>> GenerateIdiomsWithWordAsync(long languageId, string word, int? count = null)
        => await GetResponseInItemsWithWordsAsync(languageId, count, word);
}

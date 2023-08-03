using WebApi.Extensions;
using WebApi.Models.Database.Learner;
using WebApi.Models.Database.Learner.ByLanguages;
using WebApi.Services.AI;
using WebApi.Services.Learner;
using WebApi.Services.UserServices;

namespace WebApi.Managers.AI;

public class SentencesGeneratorManager : GeneratorManager<SentenceByLanguage>, ISentencesGeneratorService
{

    public SentencesGeneratorManager(IUserService userService, ILanguageService languageService) : base(userService, languageService, "sentences") { }

    protected override IEnumerable<SentenceByLanguage> FromResponseStringToItemsAsync(string response, long languageId)
        => response.ParseToSentencesByLanguageFromNumeredLanguagesStr(languageId);

    public async Task<IEnumerable<SentenceByLanguage>> GenerateSentencesAsync(string language, int? count = null)
        => await GetResponseInItemsAsync(language, count);

    public async Task<IEnumerable<SentenceByLanguage>> GenerateSentencesAsync(long languageId, int? count = null)
        => await GetResponseInItemsAsync(languageId, count);



    async Task<string> GiveSentencesAsync(long languageId, string rule, int? count)
        => $"{await GiveItemsAsync(languageId, count)}, using this rule '{rule}'";

    async Task AppendUserSentencesInput(long languageId, string rule, int? count)
        => chat.AppendUserInput($"{await GiveSentencesAsync(languageId, rule, count)}. {onlyItems}");

    async Task<IEnumerable<SentenceByLanguage>> GetResponseInSentencesAsync(long languageId, string rule, int? count)
    {
        await AppendUserSentencesInput(languageId, rule, count);
        return await GetResponseInItemsAsync(languageId);
    }

    async Task<IEnumerable<SentenceByLanguage>> GetResponseInSentencesAsync(string languageName, string rule, int? count)
    {
        long languageId = (await languageService.GetLanguageIdByNameAsync(languageName))!.Value;
        return await GetResponseInSentencesAsync(languageId, rule, count);
    }


    public async Task<IEnumerable<SentenceByLanguage>> GenerateSentencesByRuleAsync(string language, string rule, int? count = null)
       => await GetResponseInSentencesAsync(language, rule, count);

    public async Task<IEnumerable<SentenceByLanguage>> GenerateSentencesByRuleAsync(long languageId, string rule, int? count = null)
        => await GetResponseInSentencesAsync(languageId, rule, count);



    public async Task<IEnumerable<SentenceByLanguage>> GenerateSentencesWithWordAsync(string language, string word, int? count = null)
        => await GetResponseInItemsWithWordsAsync(language, count, word);

    public async Task<IEnumerable<SentenceByLanguage>> GenerateSentencesWithWordAsync(long languageId, string word, int? count = null)
        => await GetResponseInItemsWithWordsAsync(languageId, count, word);

    public async Task<IEnumerable<SentenceByLanguage>> GenerateSentencesWithWordsAsync(string language, string[] words, int? count = null)
        => await GetResponseInItemsWithWordsAsync(language, count, words);

    public async Task<IEnumerable<SentenceByLanguage>> GenerateSentencesWithWordsAsync(long languageId, string[] words, int? count = null)
        => await GetResponseInItemsWithWordsAsync(languageId, count, words);


    async Task<string> GiveSentencesAsync(long languageId, string rule, string[] words, int? count)
        => $"{await GiveSentencesAsync(languageId, rule, count)} and these words '{string.Join(',', words)}'";

    async Task AppendUserSentencesInput(long languageId, string rule, string[] words, int? count)
        => chat.AppendUserInput($"{await GiveSentencesAsync(languageId, rule, words, count)}. {onlyItems}");

    async Task<IEnumerable<SentenceByLanguage>> GetResponseInSentencesAsync(long languageId, string rule, string[] words, int? count)
    {
        await AppendUserSentencesInput(languageId, rule, words, count);
        return await GetResponseInItemsAsync(languageId);
    }

    async Task<IEnumerable<SentenceByLanguage>> GetResponseInSentencesAsync(string languageName, string rule, string[] words, int? count)
    {
        long languageId = (await languageService.GetLanguageIdByNameAsync(languageName))!.Value;
        return await GetResponseInSentencesAsync(languageId, rule, words, count);
    }


    public async Task<IEnumerable<SentenceByLanguage>> GenerateSentencesWithWordsByRuleAsync(string language, string[] words, string rule, int? count = null)
        => await GetResponseInSentencesAsync(language, rule, words, count);

    public async Task<IEnumerable<SentenceByLanguage>> GenerateSentencesWithWordsByRuleAsync(long languageId, string[] words, string rule, int? count = null)
        => await GetResponseInSentencesAsync(languageId, rule, words, count);
}

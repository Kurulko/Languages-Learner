using OpenAI_API.Chat;
using OpenAI_API;
using WebApi.Models.Database.Learner.ByLanguages;
using WebApi.Services;
using System.Text.RegularExpressions;
using WebApi.Extensions;
using WebApi.Models.Database.Learner;
using WebApi.Services.Learner;
using System.Data;
using WebApi.Services.AI;

namespace WebApi.Managers.AI;

public class ChatGPTManager : IChatGPTService
{
    readonly ISentencesGeneratorService sentencesGenerator;
    readonly IIdiomsGeneratorService idiomsGenerator;
    public ChatGPTManager(ISentencesGeneratorService sentencesGenerator, IIdiomsGeneratorService idiomsGenerator)
        => (this.sentencesGenerator, this.idiomsGenerator) = (sentencesGenerator, idiomsGenerator);


    #region Idioms


    public async Task<IEnumerable<IdiomByLanguage>> GenerateIdiomsAsync(string language, int? count = null)
        => await idiomsGenerator.GenerateIdiomsAsync(language, count);

    public async Task<IEnumerable<IdiomByLanguage>> GenerateIdiomsAsync(long languageId, int? count = null)
        => await idiomsGenerator.GenerateIdiomsAsync(languageId, count);

    public async Task<IEnumerable<IdiomByLanguage>> GenerateIdiomsWithWordAsync(string language, string word, int? count = null)
        => await idiomsGenerator.GenerateIdiomsWithWordAsync(language, word, count);

    public async Task<IEnumerable<IdiomByLanguage>> GenerateIdiomsWithWordAsync(long languageId, string word, int? count = null)
        => await idiomsGenerator.GenerateIdiomsWithWordAsync(languageId, word, count);


    #endregion


    #region Sentences


    public async Task<IEnumerable<SentenceByLanguage>> GenerateSentencesAsync(string language, int? count = null)
        => await sentencesGenerator.GenerateSentencesAsync(language, count);

    public async Task<IEnumerable<SentenceByLanguage>> GenerateSentencesAsync(long languageId, int? count = null)
        => await sentencesGenerator.GenerateSentencesAsync(languageId, count);

    public async Task<IEnumerable<SentenceByLanguage>> GenerateSentencesByRuleAsync(string language, string rule, int? count = null)
        => await sentencesGenerator.GenerateSentencesByRuleAsync(language, rule, count);

    public async Task<IEnumerable<SentenceByLanguage>> GenerateSentencesByRuleAsync(long languageId, string rule, int? count = null)
        => await sentencesGenerator.GenerateSentencesByRuleAsync(languageId, rule, count);

    public async Task<IEnumerable<SentenceByLanguage>> GenerateSentencesWithWordAsync(string language, string word, int? count = null)
        => await sentencesGenerator.GenerateSentencesWithWordAsync(language, word, count);

    public async Task<IEnumerable<SentenceByLanguage>> GenerateSentencesWithWordAsync(long languageId, string word, int? count = null)
        => await sentencesGenerator.GenerateSentencesWithWordAsync(languageId, word, count);

    public async Task<IEnumerable<SentenceByLanguage>> GenerateSentencesWithWordsAsync(string language, string[] words, int? count = null)
        => await sentencesGenerator.GenerateSentencesWithWordsAsync(language, words, count);

    public async Task<IEnumerable<SentenceByLanguage>> GenerateSentencesWithWordsAsync(long languageId, string[] words, int? count = null)
        => await sentencesGenerator.GenerateSentencesWithWordsAsync(languageId, words, count);

    public async Task<IEnumerable<SentenceByLanguage>> GenerateSentencesWithWordsByRuleAsync(string language, string[] words, string rule, int? count = null)
        => await sentencesGenerator.GenerateSentencesWithWordsByRuleAsync(language, words, rule, count);

    public async Task<IEnumerable<SentenceByLanguage>> GenerateSentencesWithWordsByRuleAsync(long languageId, string[] words, string rule, int? count = null)
        => await sentencesGenerator.GenerateSentencesWithWordsByRuleAsync(languageId, words, rule, count);


    #endregion
}

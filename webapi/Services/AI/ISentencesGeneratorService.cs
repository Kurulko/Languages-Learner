using WebApi.Models.Database.Learner.ByLanguages;

namespace WebApi.Services.AI;

public interface ISentencesGeneratorService
{
    Task<IEnumerable<SentenceByLanguage>> GenerateSentencesAsync(string language, int? count = null);
    Task<IEnumerable<SentenceByLanguage>> GenerateSentencesAsync(long languageId, int? count = null);

    Task<IEnumerable<SentenceByLanguage>> GenerateSentencesWithWordAsync(string language, string word, int? count = null);
    Task<IEnumerable<SentenceByLanguage>> GenerateSentencesWithWordAsync(long languageId, string word, int? count = null);

    Task<IEnumerable<SentenceByLanguage>> GenerateSentencesWithWordsAsync(string language, string[] words, int? count = null);
    Task<IEnumerable<SentenceByLanguage>> GenerateSentencesWithWordsAsync(long languageId, string[] words, int? count = null);

    Task<IEnumerable<SentenceByLanguage>> GenerateSentencesByRuleAsync(string language, string rule, int? count = null);
    Task<IEnumerable<SentenceByLanguage>> GenerateSentencesByRuleAsync(long languageId, string rule, int? count = null);

    Task<IEnumerable<SentenceByLanguage>> GenerateSentencesWithWordsByRuleAsync(string language, string[] words, string rule, int? count = null);
    Task<IEnumerable<SentenceByLanguage>> GenerateSentencesWithWordsByRuleAsync(long languageId, string[] words, string rule, int? count = null);
}

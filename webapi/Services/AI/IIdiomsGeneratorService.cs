using WebApi.Models.Database.Learner.ByLanguages;

namespace WebApi.Services.AI;

public interface IIdiomsGeneratorService
{
    Task<IEnumerable<IdiomByLanguage>> GenerateIdiomsAsync(string language, int? count = null);
    Task<IEnumerable<IdiomByLanguage>> GenerateIdiomsAsync(long languageId, int? count = null);

    Task<IEnumerable<IdiomByLanguage>> GenerateIdiomsWithWordAsync(string language, string word, int? count = null);
    Task<IEnumerable<IdiomByLanguage>> GenerateIdiomsWithWordAsync(long languageId, string word, int? count = null);
}

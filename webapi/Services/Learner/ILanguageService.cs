using WebApi.Models.Database.Learner;

namespace WebApi.Services.Learner;

public interface ILanguageService : ILearnerDbModelService<Language>
{
    Task<Language?> GetLanguageByNameAsync(string name);
    Task<long?> GetLanguageIdByNameAsync(string name);
    Task<string?> GetLanguageNameByIdAsync(long id);
}

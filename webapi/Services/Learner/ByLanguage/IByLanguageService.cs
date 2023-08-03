using WebApi.Models.Database.Learner.ByLanguages;

namespace WebApi.Services.Learner.ByLanguage;

public interface IByLanguageService<T> : ILearnerDbModelService<T> where T : ByLanguageModel
{
    Task<T?> GetModelByValueAsync(string value);
}

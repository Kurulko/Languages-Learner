using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using WebApi.Context;
using WebApi.Models.Database.Learner;
using WebApi.Services.Learner;

namespace WebApi.Managers.Learner;

public class LanguageManager : LearnerManager<Language>, ILanguageService
{
    public LanguageManager(LearnerContext db) : base(db) { }

    public async Task<Language?> GetLanguageByNameAsync(string name)
        => await GetAllModels().SingleOrDefaultAsync(m => m.Name == name);

    public async Task<long?> GetLanguageIdByNameAsync(string name)
        => (await GetLanguageByNameAsync(name))?.Id;

    public async Task<string?> GetLanguageNameByIdAsync(long id)
        => (await GetModelByIdAsync(id))?.Name;
}

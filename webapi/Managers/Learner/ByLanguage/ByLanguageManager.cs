using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models.Database.Learner.ByLanguages;
using WebApi.Models.Database.Learner;
using WebApi.Services.Learner.ByLanguage;
using WebApi.Services.UserServices;

namespace WebApi.Managers.Learner.ByLanguage;

public abstract class ByLanguageManager<T> : LearnerManager<T>, IByLanguageService<T> where T : ByLanguageModel
{
    readonly IUserService userService;
    protected ByLanguageManager(LearnerContext db, IUserService userService) : base(db)
        => this.userService = userService;

    protected override IQueryable<T> GetAllModels()
        => dbSet.Include(m => m.Language);

    public async Task<T?> GetModelByValueAsync(string value)
        => await GetAllModels().SingleOrDefaultAsync(m => m.Value == value);

    public override async Task<T> AddModelAsync(T model)
    {
        model.Language = null;
        model.User = null;
        model.UserId = await userService.GetUsedUserIdAsync();
        return await base.AddModelAsync(model);
    }

    public override async Task UpdateModelAsync(T model)
    {
        model.Language = null;
        model.User = null;
        model.UserId = await userService.GetUsedUserIdAsync();
        await base.UpdateModelAsync(model);
    }
}

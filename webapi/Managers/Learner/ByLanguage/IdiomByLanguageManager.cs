using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models.Database.Learner.ByLanguages;
using WebApi.Services.Learner.ByLanguage;
using WebApi.Services.UserServices;

namespace WebApi.Managers.Learner.ByLanguage;

public class IdiomByLanguageManager : ByLanguageManager<IdiomByLanguage>, IIdiomByLanguageService
{
    public IdiomByLanguageManager(LearnerContext db, IUserService userService)
        : base(db, userService) { }
}

using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models.Database.Learner.ByLanguages;
using WebApi.Services.Learner.ByLanguage;
using WebApi.Services.UserServices;

namespace WebApi.Managers.Learner.ByLanguage;

public class RuleByLanguageManager : ByLanguageManager<RuleByLanguage>, IRuleByLanguageService
{
    public RuleByLanguageManager(LearnerContext db, IUserService userService)
        : base(db, userService) { }
}

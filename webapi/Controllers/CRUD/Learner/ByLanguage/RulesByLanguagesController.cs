using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Database.Learner.ByLanguages;
using WebApi.Services.Learner.ByLanguage;

namespace WebApi.Controllers.CRUD.Learner.ByLanguage;

[Route($"{pathApi}/rules-{pathToByLanguages}")]
public class RulesByLanguagesController : ByLanguagesController<RuleByLanguage>
{
    public RulesByLanguagesController(IRuleByLanguageService service) : base(service) { }
}

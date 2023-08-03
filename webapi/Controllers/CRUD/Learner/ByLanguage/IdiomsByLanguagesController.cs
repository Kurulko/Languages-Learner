using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Database.Learner.ByLanguages;
using WebApi.Services.Learner.ByLanguage;

namespace WebApi.Controllers.CRUD.Learner.ByLanguage;

[Route($"{pathApi}/idioms-{pathToByLanguages}")]
public class IdiomsByLanguagesController : ByLanguagesController<IdiomByLanguage>
{
    public IdiomsByLanguagesController(IIdiomByLanguageService service) : base(service) { }
}

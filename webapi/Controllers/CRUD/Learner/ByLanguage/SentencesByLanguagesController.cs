using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Database.Learner.ByLanguages;
using WebApi.Services.Learner.ByLanguage;

namespace WebApi.Controllers.CRUD.Learner.ByLanguage;

[Route($"{pathApi}/sentences-{pathToByLanguages}")]
public class SentencesByLanguagesController : ByLanguagesController<SentenceByLanguage>
{
    public SentencesByLanguagesController(ISentenceByLanguageService service) : base(service) { }
}

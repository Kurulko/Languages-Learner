using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Database.Learner.ByLanguages;
using WebApi.Services.Learner.ByLanguage;

namespace WebApi.Controllers.CRUD.Learner.ByLanguage;

[Route($"{pathApi}/words-{pathToByLanguages}")]
public class WordsByLanguagesController : ByLanguagesController<WordByLanguage>
{
    public WordsByLanguagesController(IWordByLanguageService service) : base(service) { }
}

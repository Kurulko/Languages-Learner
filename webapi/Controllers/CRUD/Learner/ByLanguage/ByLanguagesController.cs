using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Database.Learner;
using WebApi.Models.Database.Learner.ByLanguages;
using WebApi.Services;
using WebApi.Services.Learner.ByLanguage;

namespace WebApi.Controllers.CRUD.Learner.ByLanguage;

public abstract class ByLanguagesController<T> : LearnerController<T> where T : ByLanguageModel
{
    protected const string pathToByLanguages = "by-languages";

    readonly IByLanguageService<T> learnerByLanguageService;
    public ByLanguagesController(IByLanguageService<T> service) : base(service)
        => learnerByLanguageService = service;


    [HttpGet("by-name/{value}")]
    public async Task<T?> GetModelByValueAsync(string value)
        => await learnerByLanguageService.GetModelByValueAsync(value);
}

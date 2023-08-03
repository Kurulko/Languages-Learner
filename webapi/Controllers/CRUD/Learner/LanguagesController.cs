using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Database.Learner;
using WebApi.Services;
using WebApi.Services.Learner;

namespace WebApi.Controllers.CRUD.Learner;

public class LanguagesController : LearnerController<Language>
{
    readonly ILanguageService languageService;
    public LanguagesController(ILanguageService service) : base(service)
        => this.languageService = service;


    [HttpGet("{name}")]
    public async Task<Language?> GetLanguageByNameAsync(string name)
        => await languageService.GetLanguageByNameAsync(name);

    [HttpGet("{name}/id")]
    public async Task<long?> GetLanguageIdByNameAsync(string name)
        => await languageService.GetLanguageIdByNameAsync(name);

    [HttpGet("{id}/name")]
    public async Task<string?> GetLanguageNameByIdAsync(long id)
        => await languageService.GetLanguageNameByIdAsync(id);
}

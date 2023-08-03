using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using WebApi.Controllers.CRUD;
using WebApi.Models.Account;
using WebApi.Models.Database;
using WebApi.Models.Database.Learner.ByLanguages;
using WebApi.Services.AI;

namespace WebApi.Controllers.AI;

[Authorize]
public class ChatGPTController : ApiController
{
    readonly IChatGPTService chatGPTService;
    public ChatGPTController(IChatGPTService chatGPTService)
        => this.chatGPTService = chatGPTService;

    
    const string idioms = "idioms";


    [HttpGet(idioms + "/{languageName}")]
    public async Task<IEnumerable<IdiomByLanguage>> GenerateIdiomsAsync(string languageName,
            [FromQuery] string? word = null, [FromQuery] int? count = null)
        => await (word is null ? 
            chatGPTService.GenerateIdiomsAsync(languageName, count) : 
            chatGPTService.GenerateIdiomsWithWordAsync(languageName, word, count));

    [HttpGet(idioms + "/{languageId:long}")]
    public async Task<IEnumerable<IdiomByLanguage>> GenerateIdiomsAsync(long languageId,
            [FromQuery] string? word = null, [FromQuery] int? count = null)
        => await (word is null ?
            chatGPTService.GenerateIdiomsAsync(languageId, count) :
            chatGPTService.GenerateIdiomsWithWordAsync(languageId, word, count));


    const string sentences = "sentences";


    [HttpGet(sentences + "/{languageName}")]
    public async Task<IEnumerable<SentenceByLanguage>> GenerateSentencesAsync(string languageName,
            [FromQuery] string[]? words = null, [FromQuery] string? rule = null, [FromQuery] int? count = null)
    {
        bool isWordsNull = words is null, isRuleNull = rule is null;

        return (isWordsNull, isRuleNull) switch
        {
            (true, true) => await chatGPTService.GenerateSentencesAsync(languageName, count),
            (true, false) => await chatGPTService.GenerateSentencesByRuleAsync(languageName, rule!, count),
            (false, true) => await chatGPTService.GenerateSentencesWithWordsAsync(languageName, words!, count),
            _ => await chatGPTService.GenerateSentencesWithWordsByRuleAsync(languageName, words!, rule!, count),
        };
    }


    [HttpGet(sentences + "/{languageId:long}")]
    public async Task<IEnumerable<SentenceByLanguage>> GenerateSentencesAsync( long languageId,
            [FromQuery] string[]? words = null, [FromQuery] string? rule = null, [FromQuery] int? count = null)
    {
        bool isWordsNull = words is null, isRuleNull = rule is null;

        return (isWordsNull, isRuleNull) switch
        {
            (true, true) => await chatGPTService.GenerateSentencesAsync(languageId, count),
            (true, false) => await chatGPTService.GenerateSentencesByRuleAsync(languageId, rule!, count),
            (false, true) => await chatGPTService.GenerateSentencesWithWordsAsync(languageId, words!, count),
            _ => await chatGPTService.GenerateSentencesWithWordsByRuleAsync(languageId, words!, rule!, count),
        };
    }
}
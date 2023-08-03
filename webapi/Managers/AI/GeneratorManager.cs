using OpenAI_API.Chat;
using OpenAI_API;
using WebApi.Services.Learner;
using WebApi.Models.Database.Learner.ByLanguages;
using WebApi.Extensions;
using WebApi.Services.UserServices;

namespace WebApi.Managers.AI;

public abstract class GeneratorManager<T> where T : ByLanguageModel
{
    protected readonly string itemsStr;
    protected readonly Conversation chat;
    protected readonly ILanguageService languageService;
    public GeneratorManager(IUserService userService, ILanguageService languageService, string itemsStr)
    {
        this.languageService = languageService;

        string chatGPTToken = "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";//userService.GetUsedUserChatGPTTokenAsync().Result;
        OpenAIAPI api = new(chatGPTToken);
        chat = api.Chat.CreateConversation();

        this.itemsStr = itemsStr;
    }

    protected abstract IEnumerable<T> FromResponseStringToItemsAsync(string response, long languageId);


    protected const string giveMe = "Give me";
    protected string onlyItems => $"Please, write only these {itemsStr}, not a word more!";
    protected string GetCountOfItemsStr(int? count)
        => $"{(count is null ? string.Empty : count)} {itemsStr}";

    protected async Task<IEnumerable<T>> GetResponseInItemsAsync(long languageId)
    {
        string response = await chat.GetResponseFromChatbotAsync();
        return FromResponseStringToItemsAsync(response, languageId);
    }

    protected async Task<string> GiveItemsAsync(long languageId, int? count)
    {
        string languageName = (await languageService.GetLanguageNameByIdAsync(languageId))!;
        return $"{giveMe} {GetCountOfItemsStr(count)} in {languageName}";
    }

    protected async Task AppendUserItemsInput(long languageId, int? count)
        => chat.AppendUserInput($"{await GiveItemsAsync(languageId, count)}. {onlyItems}");

    protected async Task<IEnumerable<T>> GetResponseInItemsAsync(long languageId, int? count)
    {
        await AppendUserItemsInput(languageId, count);
        return await GetResponseInItemsAsync(languageId);
    }

    protected async Task<IEnumerable<T>> GetResponseInItemsAsync(string languageName, int? count)
    {
        long languageId = (await languageService.GetLanguageIdByNameAsync(languageName))!.Value;
        return await GetResponseInItemsAsync(languageId, count);
    }


    protected async Task<string> GiveItemsAsync(long languageId, int? count, string[] words)
        => $"{await GiveItemsAsync(languageId, count)}, using thess words '{string.Join(',', words)}'";

    protected async Task AppendUserItemsInput(long languageId, int? count, string[] words)
        => chat.AppendUserInput($"{await GiveItemsAsync(languageId, count, words)}. {onlyItems}");

    protected async Task<IEnumerable<T>> GetResponseInItemsWithWordsAsync(long languageId, int? count, params string[] words)
    {
        await AppendUserItemsInput(languageId, count, words);
        return await GetResponseInItemsAsync(languageId);
    }

    protected async Task<IEnumerable<T>> GetResponseInItemsWithWordsAsync(string languageName, int? count, params string[] words)
    {
        long languageId = (await languageService.GetLanguageIdByNameAsync(languageName))!.Value;
        return await GetResponseInItemsWithWordsAsync(languageId, count, words);
    }
}

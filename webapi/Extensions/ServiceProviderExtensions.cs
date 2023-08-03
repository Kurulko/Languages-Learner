using WebApi.Managers.AI;
using WebApi.Managers.Learner.ByLanguage;
using WebApi.Managers.Learner;
using WebApi.Services.AI;
using WebApi.Services.Learner.ByLanguage;
using WebApi.Services.Learner;
using WebApi.Services.UserServices;
using WebApi.Managers.UserManagers;

namespace WebApi.Extensions;

public static class ServiceProviderExtensions
{
    public static void AddChatGPTService(this IServiceCollection services)
    {
        services.AddScoped<ISentencesGeneratorService, SentencesGeneratorManager>();
        services.AddScoped<IIdiomsGeneratorService, IdiomsGeneratorManager>();

        services.AddScoped<IChatGPTService, ChatGPTManager>();
    }

    public static void AddLearnerServices(this IServiceCollection services)
    {
        services.AddScoped<ILanguageService, LanguageManager>();
        services.AddScoped<ISentenceByLanguageService, SentenceByLanguageManager>();
        services.AddScoped<IWordByLanguageService, WordByLanguageManager>();
        services.AddScoped<IRuleByLanguageService, RuleByLanguageManager>();
        services.AddScoped<IIdiomByLanguageService, IdiomByLanguageManager>();
    }

    public static void AddUserServices(this IServiceCollection services)
    {
        services.AddScoped<IBaseUserService, BaseUserManager>();
        services.AddScoped<IUserRolesService, UserRolesManager>();
        services.AddScoped<IUserPasswordService, UserPasswordManager>();
        services.AddScoped<IUsedUserService, UsedUserManager>();
        services.AddScoped<IUserModelsService, UserModelsManager>();

        services.AddScoped<IUserService, UserManager>();
    }
}

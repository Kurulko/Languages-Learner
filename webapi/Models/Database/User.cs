using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;
using WebApi.Models.Database.Learner;
using WebApi.Models.Database.Learner.ByLanguages;

namespace WebApi.Models.Database;

public class User : IdentityUser, IDbModel
{
    public string ChatGPTToken { get; set; } = null!;

    public DateTime Registered { get; set; }
    public string? UsedUserId { get; set; }

    public Language? CurrentLanguage { get; set; }
    public IEnumerable<SentenceByLanguage>? SentencesByLanguages { get; set; }
    public IEnumerable<WordByLanguage>? WordsByLanguages { get; set; }
    public IEnumerable<RuleByLanguage>? RulesByLanguages { get; set; }
    public IEnumerable<IdiomByLanguage>? IdiomsByLanguages { get; set; }
}

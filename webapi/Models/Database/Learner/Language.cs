using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using WebApi.Models.Database.Learner.ByLanguages;

namespace WebApi.Models.Database.Learner;

[Index(nameof(Name), IsUnique = true)]
public class Language : ILearnerModel
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Enter the {0}")]
    [MinLength(3, ErrorMessage = "The length of the {0} should be over or equal {1} letters")]
    public string Name { get; set; } = null!;

    public IEnumerable<SentenceByLanguage>? SentencesByLanguage { get; set; }
    public IEnumerable<WordByLanguage>? WordsByLanguage { get; set; }
    public IEnumerable<IdiomByLanguage>? IdiomsByLanguage { get; set; }
    public IEnumerable<RuleByLanguage>? RulesByLanguage { get; set; }

    public IEnumerable<User>? Users { get; set; }
}

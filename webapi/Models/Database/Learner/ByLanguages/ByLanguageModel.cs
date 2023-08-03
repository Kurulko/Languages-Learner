using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Database.Learner.ByLanguages;

[Index(nameof(Value), IsUnique = true)]
public abstract class ByLanguageModel : ILearnerModel
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Enter the {0}")]
    [MinLength(2, ErrorMessage = "The length of the {0} should be over or equal {1} letters")]
    public string Value { get; set; } = null!;

    public long LanguageId { get; set; }
    public Language? Language { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
}

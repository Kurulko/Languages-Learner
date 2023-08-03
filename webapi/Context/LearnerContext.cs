using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using WebApi.Initializers.LearnerInitializers;
using WebApi.Models.Database;
using WebApi.Models.Database.Learner;
using WebApi.Models.Database.Learner.ByLanguages;

namespace WebApi.Context;

public class LearnerContext : IdentityDbContext<User, Role, string>
{
    public DbSet<Language> Languages { get; set; } = null!;
    public DbSet<SentenceByLanguage> SentencesByLanguages { get; set; } = null!;
    public DbSet<WordByLanguage> WordsByLanguages { get; set; } = null!;
    public DbSet<RuleByLanguage> RulesByLanguages { get; set; } = null!;
    public DbSet<IdiomByLanguage> IdiomsByLanguages { get; set; } = null!;


    public LearnerContext(DbContextOptions<LearnerContext> opts) : base(opts)
       => Database.EnsureCreated();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var humanLanguages = LanguagesInitializer.GetSomeLanguages();
        builder.Entity<Language>().HasData(AddValueForId(humanLanguages));

        base.OnModelCreating(builder);
    }

    IEnumerable<ILearnerModel> AddValueForId(IEnumerable<ILearnerModel> models)
        => models.Select((model, index) =>
        {
            model.Id = index + 1;
            return model;
        });
}

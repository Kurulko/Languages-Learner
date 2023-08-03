namespace WebApi.Models.Database.Learner.ByLanguages;

public class SentenceByLanguage : ByLanguageModel
{

    public string[] GetWords()
        => Value.Split(' ');
    public int CountOfWords => GetWords().Length;
    public WordByLanguage[] GetWordsByLanguage()
        => GetWords().Select(word => new WordByLanguage() { Value = word, Language = Language, LanguageId = LanguageId }).ToArray();
}

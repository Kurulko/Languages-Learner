using Azure;
using WebApi.Enums;
using WebApi.Models.Database.Learner;
using WebApi.Models.Database.Learner.ByLanguages;

namespace WebApi.Extensions;

public static class StringExtensions
{
    public static OrderBy ParseToOrderBy(this string orderByStr)
       => orderByStr?.ToLower() switch
       {
           "ascending" or "asc" => OrderBy.Ascending,
           "descending" or "desc" => OrderBy.Descending,
           _ => throw new ArgumentException("Can't parse to OrderBy")
       };

    public static bool TryParseToOrderBy(this string? orderByStr, out OrderBy? orderBy)
    {
        try
        {
            orderBy = ParseToOrderBy(orderByStr!);
            return true;
        }
        catch
        {
            orderBy = default;
            return false;
        }
    }



    public static IEnumerable<SentenceByLanguage> ParseToSentencesByLanguageFromNumeredLanguagesStr(this string numeredLanguagesStr, long languageId)
        => DivideNumeredList(numeredLanguagesStr).Select(s => new SentenceByLanguage() { LanguageId = languageId, Value = SubstringNumeration(s) });

    public static IEnumerable<IdiomByLanguage> ParseToIdiomsByLanguageFromNumeredIdiomsStr(this string numeredIdiomsStr, long languageId)
        => DivideNumeredList(numeredIdiomsStr).Select(s => new IdiomByLanguage() { LanguageId = languageId, Value = SubstringNumeration(s) });


    static IEnumerable<string> DivideNumeredList(string numeredListStr)
        => numeredListStr.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(SubstringNumeration);

    static string SubstringNumeration(string sentence)
    {
        int index = 0;
        for (; index < sentence.Length && (char.IsDigit(sentence[index]) || sentence[index] == '.' || sentence[index] == ' '); index++)
        {
        
        }
        
        return sentence.Substring(index).TrimStart();
    }
}

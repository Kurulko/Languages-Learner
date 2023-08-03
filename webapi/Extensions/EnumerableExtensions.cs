using System.Linq.Dynamic.Core;
using System.Security.Cryptography;
using WebApi.Enums;
using WebApi.Models.Helpers;

namespace WebApi.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> models, string attribute, OrderBy orderBy)
        => models.AsQueryable().OrderBy($"{attribute} {orderBy}");

    public static IndexViewModel<T> ToIndexViewModel<T>(this IEnumerable<T> models, int countOfAllModels, int? pageSize, int? pageNumber)
    {
        PageViewModel pageViewModel = new(countOfAllModels, pageNumber ?? 1, pageSize ?? countOfAllModels);
        return new IndexViewModel<T>(models, pageViewModel);
    }
}
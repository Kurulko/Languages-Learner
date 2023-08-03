namespace WebApi.Models.Helpers;

public record ModelWithUserId<T>(string UserId, T Model);

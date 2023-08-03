using Microsoft.AspNetCore.Authorization;
using WebApi.Models.Database;
using WebApi.Services;

namespace WebApi.Controllers.CRUD;

[Authorize(Roles = Roles.Admin)]
public class AdminDbModelsController<TModel, TKey> : DbModelsController<TModel, TKey> where TModel : IDbModel
{
    public AdminDbModelsController(IDbModelService<TModel, TKey> service) : base(service) { }
}
using Microsoft.AspNetCore.Mvc;
using WebApi.Enums;
using WebApi.Extensions;
using WebApi.Models.Database;
using WebApi.Models.Helpers;
using WebApi.Services;

namespace WebApi.Controllers.CRUD;

public abstract class DbModelsController<T, K> : ApiController where T : IDbModel
{
    protected readonly IDbModelService<T, K> service;
    public DbModelsController(IDbModelService<T, K> service)
        => this.service = service;

    [HttpGet]
    public virtual async Task<IndexViewModel<T>> GetModelsAsync([FromQuery]string? attribute, [FromQuery] string? orderBy, [FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        => await service.GetModelsAsync(attribute, orderBy?.ParseToOrderBy(), pageSize, pageNumber);

    [HttpGet("{key}")]
    public virtual async Task<T?> GetModelByIdAsync(K key)
        => await service.GetModelByIdAsync(key);

    [HttpPost]
    public virtual async Task<T> AddModelAsync(T model)
        => await service.AddModelAsync(model);

    [HttpPut]
    public virtual async Task<IActionResult> UpdateModelAsync(T model)
        => await ReturnOkIfEverithingIsGood(async () => await service.UpdateModelAsync(model));

    [HttpDelete("{key}")]
    public virtual async Task<IActionResult> DeleteModelAsync(K key)
        => await ReturnOkIfEverithingIsGood(async () => await service.DeleteModelAsync(key));
}
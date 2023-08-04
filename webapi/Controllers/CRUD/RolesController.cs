using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Database;
using WebApi.Services.RoleServices;

namespace WebApi.Controllers.CRUD;

public class RolesController : AdminDbModelsController<Role, string>
{
    readonly IRoleService roleService;
    public RolesController(IRoleService service) : base(service)
        => roleService = service;

    [HttpGet("role-by-default")]
    public async Task<Role> CreateRole()
        => await roleService.CreateRole();

    [HttpGet("by-name/{name}")]
    public async Task<Role?> GetRoleByNameAsync(string name)
        => await roleService.GetRoleByNameAsync(name);
}
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route($"{pathApi}/[controller]")]
public class ApiController : ControllerBase
{
    protected const string pathApi = "api";

    protected async Task<IActionResult> ReturnOkIfEverithingIsGood(Func<Task> action)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await action();
                return Ok();
            }

            return BadRequest(ModelState);
        }
        catch (Exception ex)
        {
            var innerEx = ex.InnerException;
            return BadRequest((innerEx is null ? ex : innerEx).Message);
        }
    }
}
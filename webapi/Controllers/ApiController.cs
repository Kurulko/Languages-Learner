using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Account;
using WebApi.Models.Database;
using WebApi.Services.Account;

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

    protected async Task<IActionResult> ReturnOkIfEverithingIsGood<T>(Func<Task<T>> action)
    {
        try
        {
            if (ModelState.IsValid)
                return Ok(await action());
            return BadRequest(ModelState);
        }
        catch (Exception ex)
        {
            Exception? innerEx = ex.InnerException;
            string errorsMessage = (innerEx is null ? ex : innerEx).Message;

            var errors = new Dictionary<string, string[]>
            {
                { "Exception", new string[] { errorsMessage } },
            };

            ProblemDetails problemDetails = new ()
            {
                Title = "One or more validation errors occurred.",
                Status = StatusCodes.Status400BadRequest,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Extensions = { ["errors"] = errors }
            };

            return BadRequest(problemDetails);
        }
    }
}
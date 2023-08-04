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
            await action();
            return Ok();
        }
        catch (Exception ex)
        {
            return ReturnProblemDetails(ex);
        }
    }

    protected async Task<IActionResult> ReturnOkIfEverithingIsGood<T>(Func<Task<T>> action)
    {
        try
        {
            return Ok(await action());
        }
        catch (Exception ex)
        {
            return ReturnProblemDetails(ex);
        }
    }

    IActionResult ReturnProblemDetails(Exception ex)
    {
        Exception? innerEx = ex.InnerException;
        string errorsMessage = (innerEx is null ? ex : innerEx).Message;

        var errors = new Dictionary<string, string[]>
            {
                { "Exception", new string[] { errorsMessage } },
            };

        ProblemDetails problemDetails = new()
        {
            Title = "One or more validation errors occurred.",
            Status = StatusCodes.Status400BadRequest,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Extensions = { ["errors"] = errors }
        };

        return BadRequest(problemDetails);
    }
}
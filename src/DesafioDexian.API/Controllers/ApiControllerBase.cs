using DesafioDexian.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace DesafioDexian.API.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return HandleError(result);
    }

    protected IActionResult HandleResult(Result result)
    {
        if (result.IsSuccess)
        {
            return NoContent();
        }

        return HandleError(result);
    }

    protected IActionResult HandleCreatedResult<T>(Result<T> result, string actionName, object routeValues)
    {
        if (result.IsSuccess)
        {
            return CreatedAtAction(actionName, routeValues, result.Value);
        }

        return HandleError(result);
    }

    private IActionResult HandleError(Result result)
    {
        var errorResponse = new { message = result.Error };

        return result.ErrorCode switch
        {
            ResultErrorCode.NotFound => NotFound(errorResponse),
            ResultErrorCode.ValidationError => BadRequest(errorResponse),
            ResultErrorCode.Unauthorized => Unauthorized(errorResponse),
            ResultErrorCode.Conflict => Conflict(errorResponse),
            ResultErrorCode.InternalError => StatusCode(500, errorResponse),
            _ => BadRequest(errorResponse)
        };
    }
}


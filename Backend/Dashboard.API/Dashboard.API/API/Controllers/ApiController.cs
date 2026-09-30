using Dashboard.API.Domain.ResultErrorDomain;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public abstract class ApiController : ControllerBase
    {
        protected IActionResult HandleResult<T>(Result<T> result, bool Created = false)
        {
            if (result.IsSuccess)
            {
                if (Created)
                {
                    // This returns HTTP 201 with the JSON body, but no Location header
                    return StatusCode(StatusCodes.Status201Created, result.Value);
                }
                return Ok(result.Value);
            }

            // Convert our "Error" object into standard "ProblemDetails"
            var problemDetails = new ProblemDetails
            {
                Title = result.Error.Title,
                Detail = result.Error.Description,
                Type = GetType(result.Error.Type),
                Status = GetStatusCode(result.Error.Type),
                Instance = HttpContext.Request.Path
            };

            return new ObjectResult(problemDetails)
            {
                StatusCode = problemDetails.Status
            };
        }

        private static int GetStatusCode(ErrorType errorType) => errorType switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.TooManyRequests => StatusCodes.Status429TooManyRequests,
            _ => StatusCodes.Status500InternalServerError
        };

        private static string GetType(ErrorType errorType) => errorType switch
        {
            ErrorType.NotFound => "Not Found",
            ErrorType.Conflict => "Conflict",
            ErrorType.TooManyRequests => "Too Many Requests",
            _ => "Server Failure"
        };
    }
}
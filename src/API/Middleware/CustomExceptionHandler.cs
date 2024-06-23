using Application.Errors.Common;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.Middleware;

public class CustomExceptionHandler : IExceptionHandler
{
    private readonly ILogger<CustomExceptionHandler> _logger;

    public CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        if (exception is not CustomException customException)
        {
            return false;
        }

        _logger.LogError(
            customException,
            "Custom Exception occurred: {Message}",
            customException.Error.Description
        );

        var problemDetails = new ProblemDetails
        {
            Title = customException.Error.Code,
            Detail = customException.Error.Description,
            Status = customException.Error.HttpStatusCode
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(
            problemDetails,
            cancellationToken
        );

        return true;
    }
}

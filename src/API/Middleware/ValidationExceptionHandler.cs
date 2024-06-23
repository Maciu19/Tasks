using System.Net;

using FluentValidation;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.Middleware;

public class ValidationExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ValidationExceptionHandler> _logger;

    public ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException)
        {
            return false;
        }

        _logger.LogError(exception, "A validation exception occurred: {Message}", exception.Message);

        var groupedErrors = validationException.Errors
            .GroupBy(g => g.PropertyName) 
            .ToDictionary(
                group => group.Key, 
                group => group.Select(x => x.ErrorMessage).ToArray()); 

        var problemDetails = new ValidationProblemDetails
        {
            Title = "One or more validation errors occurred.",
            Status = (int)HttpStatusCode.BadRequest,
            Errors = groupedErrors
        };

        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        await httpContext.Response.WriteAsJsonAsync(
            problemDetails,
            cancellationToken);

        return true;
    }
}

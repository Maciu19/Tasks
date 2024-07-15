using System.Net;

namespace Application.Common.Errors;

public enum ErrorType
{
    Failure,
    Validation,
    NotFound,
    Conflict,
    Authentication,
    Authorization
}

public static class ErrorTypeToHttpStatusMapper
{
    public static int GetHttpStatusCode(ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.Failure => (int)HttpStatusCode.InternalServerError,
            ErrorType.Validation => (int)HttpStatusCode.BadRequest,
            ErrorType.NotFound => (int)HttpStatusCode.NotFound,
            ErrorType.Conflict => (int)HttpStatusCode.Conflict,
            ErrorType.Authentication => (int)HttpStatusCode.Unauthorized,
            ErrorType.Authorization => (int)HttpStatusCode.Forbidden,
            _ => (int)HttpStatusCode.InternalServerError
        };
    }
}
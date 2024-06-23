namespace Application.Errors.Common;

public record Error
{
    public string Code { get; } = string.Empty;
    public string Description { get; } = string.Empty;  
    public int HttpStatusCode { get; }
    private ErrorType Type { get; }

    private Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
        HttpStatusCode = ErrorTypeToHttpStatusMapper.GetHttpStatusCode(type);
    }

    public static Error Failure(string code, string description) =>
        new(code, description, ErrorType.Failure);

    public static Error Validation(string code, string description) =>
        new(code, description, ErrorType.Validation);

    public static Error NotFound(string code, string description) =>
        new(code, description, ErrorType.NotFound);

    public static Error Conflict(string code, string description) =>
        new(code, description, ErrorType.Conflict);

    public static Error Authentication(string code, string description) =>
        new(code, description, ErrorType.Authentication);

    public static Error Authorization(string code, string description) =>
        new(code, description, ErrorType.Authorization);
}

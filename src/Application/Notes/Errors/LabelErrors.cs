using Application.Common.Errors;

namespace Application.Notes.Errors;

public class LabelErrors
{
    public static Error NotFound(string message) => Error.NotFound(
        code: "Label.NotFound",
        description: message
    );

    public static Error AlreadyExists(string message) => Error.Conflict(
        code: "Label.AlreadyExists",
        description: message
    );

    public static Error LabelDoesNotBelongToUser(Guid userId) => Error.Forbidden(
        code: "Label.DoesNotBelongToUser",
        description: $"Label does not belong to user with id {userId}"
    );
}

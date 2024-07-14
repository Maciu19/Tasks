using Application.Errors.Common;

namespace Application.Errors;

public static class NoteErrors
{
    public static Error NotFound(string message) => Error.NotFound(
        code: "Note.NotFound",
        description: message
    );
}

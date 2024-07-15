using Application.Common.Errors;

namespace Application.Notes.Errors;

public static class NoteErrors
{
    public static Error NotFound(string message) => Error.NotFound(
        code: "Note.NotFound",
        description: message
    );
}

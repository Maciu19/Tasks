using Application.Common.Errors;

namespace Application.Notes.Errors;

public static class NoteErrors
{
    public static Error NotFound(string message) => Error.NotFound(
        code: "Note.NotFound",
        description: message
    );

    public static Error OwnerOfTheNoteCannotBeCollaborator => Error.Validation(
        code: "Note.OwnerOfTheNoteCannotBeCollaborator",
        description: "Owner of the note cannot be a collaborator"
    );

    public static Error NoteDoesntContainLabel(Guid noteId, int labelId) => Error.NotFound(
        code: "Note.DoesntContainLabel",
        description: $"Note doesn't contain label with id {labelId}"
    );  
}

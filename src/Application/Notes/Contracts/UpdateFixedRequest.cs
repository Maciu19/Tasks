namespace Application.Notes.Contracts;

public record UpdateFixedRequest(
    Guid NoteId,
    int LabelId
);
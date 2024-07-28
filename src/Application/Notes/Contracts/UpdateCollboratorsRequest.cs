namespace Application.Notes.Contracts;

public record UpdateCollboratorsRequest(
    Guid NoteId,
    IEnumerable<Guid> CollaboratorsIds
);
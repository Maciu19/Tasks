namespace Application.Notes.Contracts;

public record UpdateNoteLabelsRequest(
    Guid NoteId,
    IEnumerable<int> LabelsIds
);
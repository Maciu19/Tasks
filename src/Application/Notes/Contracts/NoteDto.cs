using Domain.Notes;

namespace Application.Notes.Contracts;

public record NoteDto(
    Guid Id,
    Guid UserId,
    List<Guid> CollaboratorsIds,
    List<int> LabelsIds,
    string Title,
    string Content,
    DateTime LastEdited,
    DateTime? DueDate,
    bool Fixed,
    string? Background
)
{
    public static NoteDto FromNote(Note note)
        => new(
            note.Id,
            note.UserId,
            note.CollaboratorsIds,
            note.LabelsIds,
            note.Title,
            note.Content,
            note.LastEdited,
            note.DueDate,
            note.Fixed,
            note.Background);
}
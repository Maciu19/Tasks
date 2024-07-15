using Domain.Notes;

namespace Application.Notes.Contracts;

public record NoteDto(
    Guid Id,
    Guid UserId,
    string Title,
    string Content,
    bool Fixed,
    DateTime? DueDate)
{
    public static NoteDto FromNote(Note note)
        => new(
            note.Id,
            note.UserId,
            note.Title,
            note.Content,
            note.Fixed,
            note.DueDate);
}
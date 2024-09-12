namespace Domain.Notes;

public record NoteHistory(
    int Id,
    Guid NoteId,
    string Title,   
    string Content,
    DateTime Timestamp
)
{
    public NoteHistory(Note note) :
        this(0, note.Id, note.Title, note.Content, note.LastEdited) { }
}
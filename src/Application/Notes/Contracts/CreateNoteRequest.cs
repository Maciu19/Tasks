namespace Application.Notes.Contracts;

public record CreateNoteRequest(
    Guid UserId,
    string Title,
    string Content,
    bool Fixed,
    string? Background = null,
    DateTime? DueDate = null
);
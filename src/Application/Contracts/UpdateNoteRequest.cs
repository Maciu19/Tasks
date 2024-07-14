namespace Application.Contracts;

public record UpdateNoteRequest(
    Guid Id,
    string Title,
    string Content,
    bool Fixed,
    DateTime? DueDate);
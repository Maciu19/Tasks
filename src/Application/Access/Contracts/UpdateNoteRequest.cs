namespace Application.Access.Contracts;

public record UpdateNoteRequest(
    Guid Id,
    string Title,
    string Content,
    bool Fixed,
    string? Background, 
    DateTime? DueDate);
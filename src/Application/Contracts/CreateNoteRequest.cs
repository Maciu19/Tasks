namespace Application.Contracts;

public record CreateNoteRequest(
    Guid UserId,
    string Title,
    string Content,
    DateTime? DueDate = null
);
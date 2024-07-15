namespace Application.Notes.Contracts;

public record CreateLabelRequest(
    Guid UserId,
    string Name);
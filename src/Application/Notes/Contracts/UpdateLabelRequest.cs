namespace Application.Notes.Contracts;

public record UpdateLabelRequest(
    int Id,
    string NewName);
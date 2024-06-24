namespace Application.Contracts;

public record UserUpdateRequest(
    Guid Id,
    string NewDisplayName,
    string NewPassword
);
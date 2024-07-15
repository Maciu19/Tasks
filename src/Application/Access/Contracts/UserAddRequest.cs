namespace Application.Access.Contracts;

public record UserAddRequest(
    string Username,
    string Email,
    string Password,
    string DisplayName
);
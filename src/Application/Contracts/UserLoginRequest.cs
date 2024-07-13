namespace Application.Contracts;

public record UserLoginRequest (
    string? Email,
    string? Username,
    string Password
);
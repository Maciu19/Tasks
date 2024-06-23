using Domain.Entities;

namespace Application.Contracts;

public record UserAddRequest(
    string Username,
    string Email,
    string Password,
    string DisplayName
);
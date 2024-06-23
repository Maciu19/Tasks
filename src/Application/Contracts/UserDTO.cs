using Domain.Entities;

namespace Application.Contracts;

public record UserDTO(
    Guid Id,
    string Username,
    string Email,
    string Password,
    string DisplayName
)
{
    public static UserDTO FromUser(User user)
    {
        return new UserDTO(
            user.Id,
            user.Username,
            user.Email,
            user.Password,
            user.DisplayName
        );
    }
}

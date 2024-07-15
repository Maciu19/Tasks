using Domain.Access;

namespace Application.Access.Contracts;

public record UserDto(
    Guid Id,
    string Username,
    string Email,
    string Password,
    string DisplayName
)
{
    public static UserDto FromUser(User user)
    {
        return new UserDto(
            user.Id,
            user.Username,
            user.Email,
            user.Password,
            user.DisplayName
        );
    }
}

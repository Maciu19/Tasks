using Application.Access.Contracts;

namespace Application.Access.Services.Abstractions;

public interface IAuthorizationService
{
    Task<UserLoginResponse> Login(UserLoginRequest request);
    Task<UserLoginResponse> Refresh(RefreshRequest request);
    Task Logout(UserLogoutRequest request);
}

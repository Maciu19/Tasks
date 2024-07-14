using Application.Contracts;

namespace Application.Services;

public interface IAuthorizationService
{
    Task<UserLoginResponse> Login(UserLoginRequest request);
    Task<UserLoginResponse> Refresh(RefreshRequest request);
    Task Logout(UserLogoutRequest request);
}

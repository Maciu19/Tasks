using Application.Contracts;

namespace Application.Services;

public interface IAuthorizationService
{
    Task<string> Login(UserLoginRequest request);
}

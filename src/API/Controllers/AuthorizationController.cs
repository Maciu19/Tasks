using Application.Access.Contracts;
using Application.Access.Services.Abstractions;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthorizationController : ControllerBase
{
    private readonly IAuthorizationService _userService;

    public AuthorizationController(IAuthorizationService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<UserLoginResponse>> Login(UserLoginRequest request)
    {
        return Ok(await _userService.Login(request));
    }

    [HttpPost]
    [Route("refresh")]
    public async Task<ActionResult<UserLoginResponse>> Refresh(RefreshRequest request)
    {
        return Ok(await _userService.Refresh(request));
    }

    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> Logout(UserLogoutRequest request)
    {
        await _userService.Logout(request);

        return NoContent();
    }
}

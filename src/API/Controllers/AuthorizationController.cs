using Application.Contracts;
using Application.Services;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            string token = await _userService.Login(request);

            return Ok(token);
        }
    }
}

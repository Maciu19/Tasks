﻿using API.Controllers.Common;

using Application.Access.Contracts;
using Application.Access.Services.Abstractions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Access;

public class UsersController : ApiController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetById(Guid id)
    {
        var user = await _userService.GetByIdAsync(id);

        return user is null
            ? Problem(statusCode: StatusCodes.Status404NotFound, detail: $"User with id {id} not found")
            : Ok(UserDto.FromUser(user));
    }

    [HttpGet]
    [Route("email/{email}")]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetByEmail(string email)
    {
        var user = await _userService.GetByEmailAsync(email);

        return user is null
            ? Problem(statusCode: StatusCodes.Status404NotFound, detail: $"User with email {email} not found")
            : Ok(UserDto.FromUser(user));
    }

    [HttpGet]
    [Route("username/{username}")]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetByUsername(string username)
    {
        var user = await _userService.GetByUsernameAsync(username);

        return user is null
            ? Problem(statusCode: StatusCodes.Status404NotFound, detail: $"User with username {username} not found")
            : Ok(UserDto.FromUser(user));
    }

    [HttpGet]
    [Route("displayName/{displayName}")]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetByDisplayName(string displayName)
    {
        var user = await _userService.GetByDisplayNameAsync(displayName);

        return user is null
            ? Problem(statusCode: StatusCodes.Status404NotFound, detail: $"User with display name {displayName} not found")
            : Ok(UserDto.FromUser(user));
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserAddRequest request)
    {
        var user = await _userService.CreateAsync(request);

        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = user.Id },
            value: UserDto.FromUser(user));
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update(UserUpdateRequest request)
    {
        await _userService.UpdateAsync(request);

        return NoContent();
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _userService.DeleteAsync(id);

        return NoContent();
    }
}

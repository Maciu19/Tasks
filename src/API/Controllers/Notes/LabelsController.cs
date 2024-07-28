using API.Controllers.Common;

using Application.Notes.Contracts;
using Application.Notes.Services.Abstractions;

using Domain.Notes;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Notes;

[Authorize]
public class LabelsController : ApiController
{
    private readonly ILabelService _labelService;

    public LabelsController(ILabelService labelService)
    {
        _labelService = labelService;
    }

    [HttpGet]
    [Route("id/{id:int}")]
    public async Task<ActionResult<Label>> GetById(int id)
    {
        var label = await _labelService.GetByIdAsync(id);

        return label is null
            ? Problem(statusCode: StatusCodes.Status404NotFound, detail: $"Label with id {id} not found")
            : Ok(label);
    }

    [HttpGet]
    [Route("userId/{userId:Guid}/name/{name}")]
    public async Task<ActionResult<Label>> GetByName(Guid userId, string name)
    {
        var label = await _labelService.GetByNameAsync(userId, name);

        return label is null
            ? Problem(statusCode: StatusCodes.Status404NotFound, detail: $"Label with name {name} not found")
            : Ok(label);
    }

    [HttpGet]
    [Route("userId/{userId:Guid}")]
    public async Task<ActionResult<IEnumerable<Label>>> GetByUserId(Guid userId)
    {
        return Ok(await _labelService.GetByUserIdAsync(userId));
    }

    [HttpPost]
    public async Task<ActionResult<Label>> Create(CreateLabelRequest request)
    {
        var label = await _labelService.CreateAsync(request.UserId, request.Name);

        return CreatedAtAction(
            nameof(GetById),
            new { id = label.Id },
            label);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateLabelRequest request)
    {
        await _labelService.UpdateAsync(request.Id, request.NewName);

        return NoContent();
    }

    [HttpDelete]
    [Route("id/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _labelService.DeleteAsync(id);

        return NoContent();
    }
}

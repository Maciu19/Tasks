using Application.Contracts;
using Application.Services;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class NotesController : ControllerBase
{
    private readonly INoteService _noteService;

    public NotesController(INoteService noteService)
    {
        _noteService = noteService;
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<ActionResult<NoteDto>> GetById(Guid id)
    {
        var note = await _noteService.GetByIdAsync(id);

        return note is null
            ? Problem(statusCode: StatusCodes.Status404NotFound, detail: $"Note with id {id} not found")
            : Ok(NoteDto.FromNote(note));   
    }

    [HttpGet]
    [Route("userId/{userId:Guid}")]
    public async Task<ActionResult<IEnumerable<NoteDto>>> GetByUserId(Guid userId)
    {
        var notes = await _noteService.GetByUserIdAsync(userId);

        return Ok(notes.Select(NoteDto.FromNote));
    }

    [HttpPost]
    public async Task<ActionResult<NoteDto>> Create(CreateNoteRequest request)
    {
        var note = await _noteService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById), 
            new { id = note.Id }, 
            NoteDto.FromNote(note));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateNoteRequest request)
    {
        await _noteService.UpdateAsync(request);

        return NoContent();
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _noteService.DeleteAsync(id);

        return NoContent();
    }
}

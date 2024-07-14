using System.Data;

using Application.Contracts;
using Application.Errors;
using Application.Errors.Common;

using Domain.Entities;

using FluentValidation;

using Infrastructure.Repositories;

namespace Application.Services;

public class NoteService : INoteService
{
    private readonly INoteRepository _noteRepository;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<CreateNoteRequest> _createNoteRequestValidator;
    private readonly IValidator<UpdateNoteRequest> _updateNoteRequestValidator;

    public NoteService(
        INoteRepository noteRepository, 
        IUserRepository userRepository,
        IValidator<CreateNoteRequest> createNoteRequestValidator,
        IValidator<UpdateNoteRequest> updateNoteRequestValidator)
    {
        _noteRepository = noteRepository;
        _userRepository = userRepository;
        _createNoteRequestValidator = createNoteRequestValidator;
        _updateNoteRequestValidator = updateNoteRequestValidator;
    }

    public async Task<Note?> GetByIdAsync(Guid id)
        => await _noteRepository.GetByIdAsync(id);

    public async Task<IEnumerable<Note>> GetByUserIdAsync(Guid userId)
        => await _noteRepository.GetByUserIdAsync(userId);

    public async Task<Note> CreateAsync(CreateNoteRequest request)
    {
        await _createNoteRequestValidator.ValidateAndThrowAsync(request);

        if (_userRepository.GetByIdAsync(request.UserId) is null)
            throw new CustomException(UserErrors.NotFound($"User with id {request.UserId} not found"));

        Note note = new(
            request.UserId,
            request.Title,
            request.Content,
            request.DueDate?.ToUniversalTime());

        await _noteRepository.CreateAsync(note);

        return note;    
    }

    public async Task UpdateAsync(UpdateNoteRequest request)
    {
        await _updateNoteRequestValidator.ValidateAndThrowAsync(request);

        var note = await _noteRepository.GetByIdAsync(request.Id) ??
            throw new CustomException(NoteErrors.NotFound($"Note with id {request.Id} not found"));

        note.Update(
            request.Title, 
            request.Content, 
            request.Fixed,
            request.DueDate?.ToUniversalTime());

        await _noteRepository.UpdateAsync(note);
    }

    public async Task DeleteAsync(Guid id)
    {
        var note = await _noteRepository.GetByIdAsync(id) ??
            throw new CustomException(NoteErrors.NotFound($"Note with id {id} not found"));

        note.Deleted = true;

        await _noteRepository.UpdateAsync(note);
    }
}

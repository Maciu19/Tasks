using Application.Access.Contracts;
using Application.Access.Errors;
using Application.Common.Errors;
using Application.Notes.Contracts;
using Application.Notes.Errors;
using Application.Notes.Services.Abstractions;

using Domain.Notes;

using FluentValidation;

using Infrastructure.Access.Repositories.Abstractions;
using Infrastructure.Notes.Repositories.Abstractions;

namespace Application.Notes.Services;

public class NoteService : INoteService
{
    private readonly INoteRepository _noteRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILabelRepository _labelRepository;
    private readonly IValidator<CreateNoteRequest> _createNoteRequestValidator;
    private readonly IValidator<UpdateNoteRequest> _updateNoteRequestValidator;

    public NoteService(
        INoteRepository noteRepository,
        IUserRepository userRepository,
        ILabelRepository labelRepository,
        IValidator<CreateNoteRequest> createNoteRequestValidator,
        IValidator<UpdateNoteRequest> updateNoteRequestValidator)
    {
        _noteRepository = noteRepository;
        _userRepository = userRepository;
        _labelRepository = labelRepository;
        _createNoteRequestValidator = createNoteRequestValidator;
        _updateNoteRequestValidator = updateNoteRequestValidator;
    }

    public Task<Note?> GetByIdAsync(Guid id)
        => _noteRepository.GetByIdAsync(id);

    public Task<IEnumerable<Note>> GetByUserIdAsync(Guid userId)
        => _noteRepository.GetByUserIdAsync(userId);

    public async Task<Note> CreateAsync(CreateNoteRequest request)
    {
        await _createNoteRequestValidator.ValidateAndThrowAsync(request);

        if (await _userRepository.GetByIdAsync(request.UserId) is null)
            throw new CustomException(UserErrors.NotFound($"User with id {request.UserId} not found"));

        Note note = new(
            userId: request.UserId,
            title: request.Title,
            content: request.Content,
            lastEdited: DateTime.UtcNow,
            fix: request.Fixed,
            background: request.Background,
            dueDate: request.DueDate?.ToUniversalTime());

        await _noteRepository.CreateAsync(note);

        return note;
    }

    public async Task UpdateAsync(UpdateNoteRequest request)
    {
        await _updateNoteRequestValidator.ValidateAndThrowAsync(request);

        var note = await _noteRepository.GetByIdAsync(request.Id) ??
            throw new CustomException(NoteErrors.NotFound($"Note with id {request.Id} not found"));

        note.Update(
            title: request.Title,
            content: request.Content,
            lastEdited: DateTime.UtcNow,
            fix: request.Fixed,
            background: request.Background,
            dueDate: request.DueDate?.ToUniversalTime());

        await _noteRepository.UpdateAsync(note);
    }

    public async Task UpdateCollaboratorsAsync(UpdateCollboratorsRequest request)
    {
        var note = await _noteRepository.GetByIdAsync(request.NoteId) ??
            throw new CustomException(NoteErrors.NotFound($"Note with id {request.NoteId} not found"));

        if (request.CollaboratorsIds.Contains(note.UserId))
            throw new CustomException(NoteErrors.OwnerOfTheNoteCannotBeCollaborator);

        foreach (var collaboratorId in request.CollaboratorsIds)
        {
            if (await _userRepository.GetByIdAsync(collaboratorId) is null)
                throw new CustomException(UserErrors.NotFound($"User with id {collaboratorId} not found"));
        }

        await _noteRepository.UpdateCollaboratorsAsync(note, request.CollaboratorsIds);
    }

    public async Task UpdateLabelsAsync(UpdateNoteLabelsRequest request)
    {
        var note = await _noteRepository.GetByIdAsync(request.NoteId) ??
            throw new CustomException(NoteErrors.NotFound($"Note with id {request.NoteId} not found"));

        foreach (var labelId in request.LabelsIds)
        {
            var label = await _labelRepository.GetByIdAsync(labelId)
                ?? throw new CustomException(LabelErrors.NotFound($"Label with id {labelId} not found"));

            if (label.UserId != note.UserId)
                throw new CustomException(LabelErrors.LabelDoesNotBelongToUser(note.UserId));
        }

        await _noteRepository.UpdateLabelsAsync(note, request.LabelsIds);
    }

    public async Task DeleteAsync(Guid id)
    {
        var note = await _noteRepository.GetByIdAsync(id) ??
            throw new CustomException(NoteErrors.NotFound($"Note with id {id} not found"));

        note.Deleted = true;

        await _noteRepository.UpdateAsync(note);
    }
}

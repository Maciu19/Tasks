using Application.Access.Contracts;
using Application.Notes.Contracts;

using Domain.Notes;

namespace Application.Notes.Services.Abstractions;

public interface INoteService
{
    Task<IEnumerable<Note>> GetByUserIdAsync(Guid userId);
    Task<Note?> GetByIdAsync(Guid id);
    Task<Note> CreateAsync(CreateNoteRequest request);
    Task UpdateAsync(UpdateNoteRequest request);
    Task UpdateCollaboratorsAsync(UpdateCollboratorsRequest request);
    Task UpdateLabelsAsync(UpdateNoteLabelsRequest request);
    Task DeleteAsync(Guid id);
}

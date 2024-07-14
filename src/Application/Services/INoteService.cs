using Application.Contracts;

using Domain.Entities;

namespace Application.Services;

public interface INoteService
{
    Task<IEnumerable<Note>> GetByUserIdAsync(Guid userId);
    Task<Note?> GetByIdAsync(Guid id);
    Task<Note> CreateAsync(CreateNoteRequest request);
    Task UpdateAsync(UpdateNoteRequest request);
    Task DeleteAsync(Guid id);
}

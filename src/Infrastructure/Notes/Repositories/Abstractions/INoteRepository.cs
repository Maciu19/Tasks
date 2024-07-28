using Domain.Notes;

namespace Infrastructure.Notes.Repositories.Abstractions;

public interface INoteRepository
{
    Task<IEnumerable<Note>> GetByUserIdAsync(Guid userId);
    Task<Note?> GetByIdAsync(Guid id);
    Task<int> CreateAsync(Note note);
    Task<int> UpdateAsync(Note note);
    Task UpdateCollaboratorsAsync(Note note, IEnumerable<Guid> collaboratorsIds);
    Task UpdateLabelsAsync(Note note, IEnumerable<int> labelsIds);
}

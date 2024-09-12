using Domain.Notes;

namespace Infrastructure.Notes.Repositories.Abstractions;

public interface INoteRepository
{
    Task<IEnumerable<Note>> GetByUserIdAsync(Guid userId);
    Task<Note?> GetByIdAsync(Guid id);
    Task<IEnumerable<NoteHistory>> GetNoteHistoryById(Guid noteId);
    Task CreateAsync(Note note);
    Task UpdateAsync(Note note);
    Task UpdateCollaboratorsAsync(Note note, IEnumerable<Guid> collaboratorsIds);
    Task UpdateLabelsAsync(Note note, IEnumerable<int> labelsIds);
}

using Domain.Entities;

namespace Infrastructure.Repositories;

public interface INoteRepository
{
    Task<IEnumerable<Note>> GetByUserIdAsync(Guid userId);
    Task<Note?> GetByIdAsync(Guid id);
    Task<int> CreateAsync(Note note);
    Task<int> UpdateAsync(Note note);
}

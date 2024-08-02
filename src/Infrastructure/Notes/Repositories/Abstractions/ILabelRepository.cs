using Domain.Notes;

namespace Infrastructure.Notes.Repositories.Abstractions;

public interface ILabelRepository
{
    Task<IEnumerable<Label>> GetByUserIdAsync(Guid userId);
    Task<Label?> GetByIdAsync(int id);
    Task<Label?> GetByNameAsync(Guid userId, string name);
    Task<int> InsertAsync(Guid userId, string name);
    Task<int> UpdateAsync(int id, string newName);
    Task<int> UpdateFixedAsync(Guid noteId, int labelId, bool fix);
    Task<int> DeleteAsync(int id);
}

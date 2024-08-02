using Domain.Notes;

namespace Application.Notes.Services.Abstractions;

public interface ILabelService
{
    Task<IEnumerable<Label>> GetByUserIdAsync(Guid userId);
    Task<Label?> GetByIdAsync(int id);
    Task<Label?> GetByNameAsync(Guid userId, string name);
    Task<Label> CreateAsync(Guid userId, string name);
    Task UpdateAsync(int id, string newName);
    Task UpdateFixedAsync(Guid noteId, int labelId);
    Task DeleteAsync(int id);
}

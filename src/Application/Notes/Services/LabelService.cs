using Application.Access.Errors;
using Application.Access.Services.Abstractions;
using Application.Common.Errors;
using Application.Notes.Errors;
using Application.Notes.Services.Abstractions;

using Domain.Notes;

using Infrastructure.Notes.Repositories.Abstractions;

namespace Application.Notes.Services;

public class LabelService : ILabelService
{
    private readonly ILabelRepository _labelRepository;
    private readonly IUserService _userService;

    public LabelService(
        ILabelRepository labelRepository,
        IUserService userService)
    {
        _labelRepository = labelRepository;
        _userService = userService;
    }

    public Task<IEnumerable<Label>> GetByUserIdAsync(Guid userId)
       => _labelRepository.GetByUserIdAsync(userId);

    public Task<Label?> GetByIdAsync(int id)
        => _labelRepository.GetByIdAsync(id);

    public Task<Label?> GetByNameAsync(Guid userId, string name)
        => _labelRepository.GetByNameAsync(userId, name);

    public async Task<Label> CreateAsync(Guid userId, string name)
    {
        if (await _userService.GetByIdAsync(userId) is null)
            throw new CustomException(UserErrors.NotFound($"User with id {userId} not found"));

        if (await _labelRepository.GetByNameAsync(userId, name) is not null)
            throw new CustomException(LabelErrors.AlreadyExists($"Already exists a label with name {name}"));

        Label label = new(userId, name);

        label.Id = await _labelRepository.InsertAsync(userId, name);

        return label;
    }

    public async Task UpdateAsync(int id, string newName)
    {
        var label = await _labelRepository.GetByIdAsync(id) ??
            throw new CustomException(LabelErrors.NotFound($"Label with id {id} not found"));

        if (await _labelRepository.GetByNameAsync(label.UserId, newName) is not null)
            throw new CustomException(LabelErrors.AlreadyExists($"Already exists a label with name {newName}"));

        await _labelRepository.UpdateAsync(id, newName);
    }

    public async Task DeleteAsync(int id)
    {
        if (await _labelRepository.GetByIdAsync(id) is null)
            throw new CustomException(LabelErrors.NotFound($"Label with id {id} not found"));

        await _labelRepository.DeleteAsync(id);
    }
}

using Application.Notes.Contracts;

using FluentValidation;

namespace Application.Notes.Validators;

public class NoteAddRequestValidator : AbstractValidator<CreateNoteRequest>
{
    public NoteAddRequestValidator()
    {
        RuleFor(x => x.Title).NotNull().NotEmpty();
        RuleFor(x => x.Content).NotNull().NotEmpty();
        RuleFor(x => x.DueDate).ExclusiveBetween(DateTime.Now, DateTime.MaxValue).When(x => x.DueDate.HasValue);
    }
}

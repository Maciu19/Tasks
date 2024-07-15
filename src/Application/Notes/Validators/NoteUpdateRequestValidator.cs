using Application.Access.Contracts;

using FluentValidation;

namespace Application.Notes.Validators;

public class NoteUpdateRequestValidator : AbstractValidator<UpdateNoteRequest>
{
    public NoteUpdateRequestValidator()
    {
        RuleFor(x => x.Title).NotNull().NotEmpty();
        RuleFor(x => x.Content).NotNull().NotEmpty();
        RuleFor(x => x.DueDate).ExclusiveBetween(DateTime.Now, DateTime.MaxValue).When(x => x.DueDate.HasValue);
    }
}

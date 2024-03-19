using FluentValidation;

namespace Application.Features.Polyclinics.Commands.Delete;

public class DeletePolyclinicCommandValidator : AbstractValidator<DeletePolyclinicCommand>
{
    public DeletePolyclinicCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}

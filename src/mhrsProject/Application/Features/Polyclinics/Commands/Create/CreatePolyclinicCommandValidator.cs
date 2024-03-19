using FluentValidation;

namespace Application.Features.Polyclinics.Commands.Create;

public class CreatePolyclinicCommandValidator : AbstractValidator<CreatePolyclinicCommand>
{
    public CreatePolyclinicCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}

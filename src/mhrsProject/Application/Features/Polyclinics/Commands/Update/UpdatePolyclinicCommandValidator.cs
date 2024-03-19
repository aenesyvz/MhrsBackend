using FluentValidation;

namespace Application.Features.Polyclinics.Commands.Update;

public class UpdatePolyclinicCommandValidator : AbstractValidator<UpdatePolyclinicCommand>
{
    public UpdatePolyclinicCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
    }
}

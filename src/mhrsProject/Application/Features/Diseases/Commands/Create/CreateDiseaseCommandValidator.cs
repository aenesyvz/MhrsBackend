using FluentValidation;

namespace Application.Features.Diseases.Commands.Create;

public class CreateDiseaseCommandValidator : AbstractValidator<CreateDiseaseCommand>
{
    public CreateDiseaseCommandValidator()
    {
        RuleFor(c => c.PolyclinicId).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
    }
}

using FluentValidation;

namespace Application.Features.Diseases.Commands.Update;

public class UpdateDiseaseCommandValidator : AbstractValidator<UpdateDiseaseCommand>
{
    public UpdateDiseaseCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.PolyclinicId).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
    }
}

using FluentValidation;

namespace Application.Features.Diseases.Commands.Delete;

public class DeleteDiseaseCommandValidator : AbstractValidator<DeleteDiseaseCommand>
{
    public DeleteDiseaseCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}

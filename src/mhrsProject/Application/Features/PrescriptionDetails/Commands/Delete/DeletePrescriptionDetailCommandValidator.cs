using FluentValidation;

namespace Application.Features.PrescriptionDetails.Commands.Delete;

public class DeletePrescriptionDetailCommandValidator : AbstractValidator<DeletePrescriptionDetailCommand>
{
    public DeletePrescriptionDetailCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}

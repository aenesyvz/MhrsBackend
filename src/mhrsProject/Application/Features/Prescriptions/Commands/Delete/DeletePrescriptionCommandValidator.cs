using FluentValidation;

namespace Application.Features.Prescriptions.Commands.Delete;

public class DeletePrescriptionCommandValidator : AbstractValidator<DeletePrescriptionCommand>
{
    public DeletePrescriptionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}

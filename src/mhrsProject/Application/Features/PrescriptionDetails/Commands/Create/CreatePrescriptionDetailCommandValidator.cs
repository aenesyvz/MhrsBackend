using FluentValidation;

namespace Application.Features.PrescriptionDetails.Commands.Create;

public class CreatePrescriptionDetailCommandValidator : AbstractValidator<CreatePrescriptionDetailCommand>
{
    public CreatePrescriptionDetailCommandValidator()
    {
        RuleFor(c => c.PrescriptionId).NotEmpty();
        RuleFor(c => c.MedicineId).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.Dose).NotEmpty();
        RuleFor(c => c.Period).NotEmpty();
        RuleFor(c => c.UsageType).NotEmpty();
        RuleFor(c => c.UsageCount).NotEmpty();
    }
}

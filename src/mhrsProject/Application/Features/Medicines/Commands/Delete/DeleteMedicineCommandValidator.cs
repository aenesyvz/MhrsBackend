using FluentValidation;

namespace Application.Features.Medicines.Commands.Delete;

public class DeleteMedicineCommandValidator : AbstractValidator<DeleteMedicineCommand>
{
    public DeleteMedicineCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}

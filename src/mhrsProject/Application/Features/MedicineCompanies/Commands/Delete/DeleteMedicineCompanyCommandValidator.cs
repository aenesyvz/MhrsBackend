using FluentValidation;

namespace Application.Features.MedicineCompanies.Commands.Delete;

public class DeleteMedicineCompanyCommandValidator : AbstractValidator<DeleteMedicineCompanyCommand>
{
    public DeleteMedicineCompanyCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}

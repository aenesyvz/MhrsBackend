using FluentValidation;

namespace Application.Features.MedicineCompanies.Commands.Create;

public class CreateMedicineCompanyCommandValidator : AbstractValidator<CreateMedicineCompanyCommand>
{
    public CreateMedicineCompanyCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Address).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.PhoneNumber).NotEmpty();
        RuleFor(c => c.TaxOffice).NotEmpty();
        RuleFor(c => c.TaxNumber).NotEmpty();
    }
}

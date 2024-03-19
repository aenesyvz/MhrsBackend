using FluentValidation;

namespace Application.Features.MedicineCompanies.Commands.Update;

public class UpdateMedicineCompanyCommandValidator : AbstractValidator<UpdateMedicineCompanyCommand>
{
    public UpdateMedicineCompanyCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Address).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.PhoneNumber).NotEmpty();
        RuleFor(c => c.TaxOffice).NotEmpty();
        RuleFor(c => c.TaxNumber).NotEmpty();
        RuleFor(c => c.ImageUrl).NotEmpty();
    }
}

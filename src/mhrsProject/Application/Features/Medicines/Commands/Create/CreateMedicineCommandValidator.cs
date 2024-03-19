using FluentValidation;

namespace Application.Features.Medicines.Commands.Create;

public class CreateMedicineCommandValidator : AbstractValidator<CreateMedicineCommand>
{
    public CreateMedicineCommandValidator()
    {
        RuleFor(c => c.MedicineCompanyId).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.PurposeOfUsage).NotEmpty();
        RuleFor(c => c.SideEffects).NotEmpty();
        RuleFor(c => c.ConditionsToBeConsidired).NotEmpty();
        RuleFor(c => c.TermsOfUse).NotEmpty();
    }
}

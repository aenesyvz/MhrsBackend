using FluentValidation;

namespace Application.Features.Medicines.Commands.Update;

public class UpdateMedicineCommandValidator : AbstractValidator<UpdateMedicineCommand>
{
    public UpdateMedicineCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.MedicineCompanyId).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.PurposeOfUsage).NotEmpty();
        RuleFor(c => c.SideEffects).NotEmpty();
        RuleFor(c => c.ConditionsToBeConsidired).NotEmpty();
        RuleFor(c => c.TermsOfUse).NotEmpty();
    }
}

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Medicine : Entity<Guid>
{
    public Guid MedicineCompanyId { get; set; }
    public string Name { get; set; }
    public string PurposeOfUsage { get; set; }
    public string SideEffects { get; set; }
    public string ConditionsToBeConsidired { get; set; }
    public string TermsOfUse { get; set; }
    public string? ImageUrl { get; set; }

    public virtual MedicineCompany? MedicineCompany { get; set; }

    public Medicine()
    {

    }

    public Medicine(Guid id, Guid medicineCompanyId, string name, string purposeOfUsage, string sideEffects, string conditionsToBeConsidired, string termsOfUse, string? ımageUrl) : this()
    {
        Id = id;
        MedicineCompanyId = medicineCompanyId;
        Name = name;
        PurposeOfUsage = purposeOfUsage;
        SideEffects = sideEffects;
        ConditionsToBeConsidired = conditionsToBeConsidired;
        TermsOfUse = termsOfUse;
        ImageUrl = ımageUrl;
    }
}

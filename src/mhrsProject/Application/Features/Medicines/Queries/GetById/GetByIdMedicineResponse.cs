using Core.Application.Responses;

namespace Application.Features.Medicines.Queries.GetById;

public class GetByIdMedicineResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid MedicineCompanyId { get; set; }
    public string Name { get; set; }
    public string PurposeOfUsage { get; set; }
    public string SideEffects { get; set; }
    public string ConditionsToBeConsidired { get; set; }
    public string TermsOfUse { get; set; }
}
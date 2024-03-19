using Core.Application.Dtos;

namespace Application.Features.Medicines.Queries.GetList;

public class GetListMedicineListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid MedicineCompanyId { get; set; }
    public string Name { get; set; }
    public string PurposeOfUsage { get; set; }
    public string SideEffects { get; set; }
    public string ConditionsToBeConsidired { get; set; }
    public string TermsOfUse { get; set; }
}

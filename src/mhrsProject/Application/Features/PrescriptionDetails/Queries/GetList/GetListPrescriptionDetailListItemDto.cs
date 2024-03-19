using Core.Application.Dtos;

namespace Application.Features.PrescriptionDetails.Queries.GetList;

public class GetListPrescriptionDetailListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid PrescriptionId { get; set; }
    public Guid MedicineId { get; set; }
    public string Description { get; set; }
    public int Dose { get; set; }
    public int Period { get; set; }
    public string UsageType { get; set; }
    public int UsageCount { get; set; }
}

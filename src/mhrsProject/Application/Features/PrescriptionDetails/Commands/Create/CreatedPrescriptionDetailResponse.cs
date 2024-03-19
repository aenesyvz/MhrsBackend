using Core.Application.Responses;

namespace Application.Features.PrescriptionDetails.Commands.Create;

public class CreatedPrescriptionDetailResponse : IResponse
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

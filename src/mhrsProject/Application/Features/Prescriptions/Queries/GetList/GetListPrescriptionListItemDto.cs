using Core.Application.Dtos;

namespace Application.Features.Prescriptions.Queries.GetList;

public class GetListPrescriptionListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid HospitalId { get; set; }
    public Guid DoctorId { get; set; }
    public string PrescriptionType { get; set; }
}

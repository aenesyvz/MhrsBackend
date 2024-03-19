using Core.Application.Responses;

namespace Application.Features.Prescriptions.Queries.GetById;

public class GetByIdPrescriptionResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid HospitalId { get; set; }
    public Guid DoctorId { get; set; }
    public string PrescriptionType { get; set; }
}

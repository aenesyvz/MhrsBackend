using Core.Application.Responses;

namespace Application.Features.Prescriptions.Commands.Create;

public class CreatedPrescriptionResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid HospitalId { get; set; }
    public Guid DoctorId { get; set; }
    public string PrescriptionType { get; set; }
}

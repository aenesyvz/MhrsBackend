using Core.Application.Responses;

namespace Application.Features.Appointments.Queries.GetById;

public class GetByIdAppointmentResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid AppointmentTimeId { get; set; }
    public Guid HospitalId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid PolyclinicId { get; set; }
    public Guid PatientId { get; set; }
}

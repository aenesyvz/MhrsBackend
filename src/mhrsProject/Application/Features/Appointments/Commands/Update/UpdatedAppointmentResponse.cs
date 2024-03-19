using Core.Application.Responses;

namespace Application.Features.Appointments.Commands.Update;

public class UpdatedAppointmentResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid AppointmentTimeId { get; set; }
    public Guid HospitalId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid PolyclinicId { get; set; }
    public Guid PatientId { get; set; }
}

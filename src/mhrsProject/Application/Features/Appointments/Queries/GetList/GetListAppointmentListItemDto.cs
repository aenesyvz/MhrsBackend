using Core.Application.Dtos;

namespace Application.Features.Appointments.Queries.GetList;

public class GetListAppointmentListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid AppointmentTimeId { get; set; }
    public Guid HospitalId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid PolyclinicId { get; set; }
    public Guid PatientId { get; set; }
}

using FluentValidation;

namespace Application.Features.Appointments.Commands.Create;

public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentCommandValidator()
    {
        RuleFor(c => c.AppointmentTimeId).NotEmpty();
        RuleFor(c => c.HospitalId).NotEmpty();
        RuleFor(c => c.DoctorId).NotEmpty();
        RuleFor(c => c.PolyclinicId).NotEmpty();
        RuleFor(c => c.PatientId).NotEmpty();
        RuleFor(c => c.Date).NotEmpty();
    }
}

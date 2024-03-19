using Application.Features.Appointments.Constants;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Appointments.Commands.Update;


public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
{
    public UpdateAppointmentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.AppointmentTimeId).NotEmpty();
        RuleFor(c => c.HospitalId).NotEmpty();
        RuleFor(c => c.DoctorId).NotEmpty();
        RuleFor(c => c.PolyclinicId).NotEmpty();
        RuleFor(c => c.PatientId).NotEmpty();
    }
}

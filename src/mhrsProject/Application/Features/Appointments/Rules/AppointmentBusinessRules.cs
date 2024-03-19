using Application.Features.Appointments.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Appointments.Rules;
public class AppointmentBusinessRules : BaseBusinessRules
{
    private readonly IAppointmentRepository _appointmentRepository;

    public AppointmentBusinessRules(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public Task AppointmentShouldExistWhenSelected(Appointment? appointment)
    {
        if (appointment == null)
            throw new BusinessException(AppointmentsBusinessMessages.AppointmentNotExists);
        return Task.CompletedTask;
    }

    public async Task AppointmentIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Appointment? appointment = await _appointmentRepository.GetAsync(
            predicate: a => a.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await AppointmentShouldExistWhenSelected(appointment);
    }
}

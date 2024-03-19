using Application.Features.AppointmentTimes.Contants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AppointmentTimes.Rules;
public class AppointmentTimeBusinessRules : BaseBusinessRules
{
    private readonly IAppointmentTimeRepository _appointmentTimeRepository;

    public AppointmentTimeBusinessRules(IAppointmentTimeRepository appointmentTimeRepository)
    {
        _appointmentTimeRepository = appointmentTimeRepository;
    }

    public Task AppointmentTimeShouldExistWhenSelected(AppointmentTime? appointmentTime)
    {
        if (appointmentTime == null)
            throw new BusinessException(AppointmentTimesBusinessMessages.AppointmentTimeNotExists);
        return Task.CompletedTask;
    }

    public async Task AppointmentTimeIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        AppointmentTime? appointmentTime = await _appointmentTimeRepository.GetAsync(
            predicate: at => at.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );

        await AppointmentTimeShouldExistWhenSelected(appointmentTime);
    }

    public async Task AppointmentTimeCannotBeDuplicateWhenInsertedOrUpdated(int hour, int minute)
    {
        AppointmentTime? appointmentTime = await _appointmentTimeRepository.GetAsync(
                    predicate: at => at.Hour == hour && at.Minute == minute
                );

        if (appointmentTime != null)
        {
            throw new BusinessException(AppointmentTimesBusinessMessages.AppointmentTimeExists);
        }
    }

}
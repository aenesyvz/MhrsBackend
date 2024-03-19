using Application.Features.AppointmentTimes.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.AppointmentTimes;

public class AppointmentTimesManager : IAppointmentTimesService
{
    private readonly IAppointmentTimeRepository _appointmentTimeRepository;
    private readonly AppointmentTimeBusinessRules _appointmentTimeBusinessRules;

    public AppointmentTimesManager(IAppointmentTimeRepository appointmentTimeRepository, AppointmentTimeBusinessRules appointmentTimeBusinessRules)
    {
        _appointmentTimeRepository = appointmentTimeRepository;
        _appointmentTimeBusinessRules = appointmentTimeBusinessRules;
    }

    public async Task<AppointmentTime?> GetAsync(
        Expression<Func<AppointmentTime, bool>> predicate,
        Func<IQueryable<AppointmentTime>, IIncludableQueryable<AppointmentTime, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        AppointmentTime? appointmentTime = await _appointmentTimeRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return appointmentTime;
    }

    public async Task<IPaginate<AppointmentTime>?> GetListAsync(
        Expression<Func<AppointmentTime, bool>>? predicate = null,
        Func<IQueryable<AppointmentTime>, IOrderedQueryable<AppointmentTime>>? orderBy = null,
        Func<IQueryable<AppointmentTime>, IIncludableQueryable<AppointmentTime, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<AppointmentTime> appointmentTimeList = await _appointmentTimeRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return appointmentTimeList;
    }

    public async Task<AppointmentTime> AddAsync(AppointmentTime appointmentTime)
    {
        AppointmentTime addedAppointmentTime = await _appointmentTimeRepository.AddAsync(appointmentTime);

        return addedAppointmentTime;
    }

    public async Task<AppointmentTime> UpdateAsync(AppointmentTime appointmentTime)
    {
        AppointmentTime updatedAppointmentTime = await _appointmentTimeRepository.UpdateAsync(appointmentTime);

        return updatedAppointmentTime;
    }

    public async Task<AppointmentTime> DeleteAsync(AppointmentTime appointmentTime, bool permanent = false)
    {
        AppointmentTime deletedAppointmentTime = await _appointmentTimeRepository.DeleteAsync(appointmentTime);

        return deletedAppointmentTime;
    }
}

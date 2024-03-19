using Application.Features.Appointments.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Appointments;

public class AppointmentsManager : IAppointmentsService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly AppointmentBusinessRules _appointmentBusinessRules;

    public AppointmentsManager(IAppointmentRepository appointmentRepository, AppointmentBusinessRules appointmentBusinessRules)
    {
        _appointmentRepository = appointmentRepository;
        _appointmentBusinessRules = appointmentBusinessRules;
    }

    public async Task<Appointment?> GetAsync(
        Expression<Func<Appointment, bool>> predicate,
        Func<IQueryable<Appointment>, IIncludableQueryable<Appointment, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Appointment? appointment = await _appointmentRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return appointment;
    }

    public async Task<IPaginate<Appointment>?> GetListAsync(
        Expression<Func<Appointment, bool>>? predicate = null,
        Func<IQueryable<Appointment>, IOrderedQueryable<Appointment>>? orderBy = null,
        Func<IQueryable<Appointment>, IIncludableQueryable<Appointment, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Appointment> appointmentList = await _appointmentRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return appointmentList;
    }

    public async Task<Appointment> AddAsync(Appointment appointment)
    {
        Appointment addedAppointment = await _appointmentRepository.AddAsync(appointment);

        return addedAppointment;
    }

    public async Task<Appointment> UpdateAsync(Appointment appointment)
    {
        Appointment updatedAppointment = await _appointmentRepository.UpdateAsync(appointment);

        return updatedAppointment;
    }

    public async Task<Appointment> DeleteAsync(Appointment appointment, bool permanent = false)
    {
        Appointment deletedAppointment = await _appointmentRepository.DeleteAsync(appointment);

        return deletedAppointment;
    }
}

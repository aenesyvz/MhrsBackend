using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AppointmentTimes;
public interface IAppointmentTimesService
{
    Task<AppointmentTime?> GetAsync(
        Expression<Func<AppointmentTime, bool>> predicate,
        Func<IQueryable<AppointmentTime>, IIncludableQueryable<AppointmentTime, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<AppointmentTime>?> GetListAsync(
        Expression<Func<AppointmentTime, bool>>? predicate = null,
        Func<IQueryable<AppointmentTime>, IOrderedQueryable<AppointmentTime>>? orderBy = null,
        Func<IQueryable<AppointmentTime>, IIncludableQueryable<AppointmentTime, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<AppointmentTime> AddAsync(AppointmentTime appointmentTime);
    Task<AppointmentTime> UpdateAsync(AppointmentTime appointmentTime);
    Task<AppointmentTime> DeleteAsync(AppointmentTime appointmentTime, bool permanent = false);
}

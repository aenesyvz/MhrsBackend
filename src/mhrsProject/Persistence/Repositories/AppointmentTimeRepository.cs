using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class AppointmentTimeRepository : EfRepositoryBase<AppointmentTime, Guid, BaseDbContext>, IAppointmentTimeRepository
{
    public AppointmentTimeRepository(BaseDbContext context) : base(context)
    {
    }

    public async Task<List<AppointmentTime>> GetListWithoutPaginationAsync()
    {
        var appointmentTimes = await Query().AsNoTracking().OrderBy(at => at.Hour).ThenBy(at => at.Minute).ToListAsync();
        return appointmentTimes;
    }
}

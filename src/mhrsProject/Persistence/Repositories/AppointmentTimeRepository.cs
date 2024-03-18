using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class AppointmentTimeRepository : EfRepositoryBase<AppointmentTime, Guid, BaseDbContext>, IAppointmentTimeRepository
{
    public AppointmentTimeRepository(BaseDbContext context) : base(context)
    {
    }
}

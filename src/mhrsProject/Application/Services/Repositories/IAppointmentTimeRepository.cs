using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IAppointmentTimeRepository : IAsyncRepository<AppointmentTime, Guid>, IRepository<AppointmentTime, Guid>
{
    Task<List<AppointmentTime>> GetListWithoutPaginationAsync();
}

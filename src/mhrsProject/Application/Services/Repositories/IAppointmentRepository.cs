using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IAppointmentRepository : IAsyncRepository<Appointment, Guid>, IRepository<Appointment, Guid>
{
}

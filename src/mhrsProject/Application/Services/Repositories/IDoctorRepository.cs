using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IDoctorRepository : IAsyncRepository<Doctor, Guid>, IRepository<Doctor, Guid>
{
}

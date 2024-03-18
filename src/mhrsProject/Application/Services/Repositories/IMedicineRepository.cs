using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IMedicineRepository : IAsyncRepository<Medicine, Guid>, IRepository<Medicine, Guid>
{
}

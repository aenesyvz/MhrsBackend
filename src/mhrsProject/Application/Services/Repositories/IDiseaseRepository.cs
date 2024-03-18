using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IDiseaseRepository : IAsyncRepository<Disease, Guid>, IRepository<Disease, Guid>
{
}

using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IHospitalRepository : IAsyncRepository<Hospital, Guid>, IRepository<Hospital, Guid>
{
}

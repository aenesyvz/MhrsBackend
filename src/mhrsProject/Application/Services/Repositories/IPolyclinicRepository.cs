using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IPolyclinicRepository : IAsyncRepository<Polyclinic, Guid>, IRepository<Polyclinic, Guid>
{
}

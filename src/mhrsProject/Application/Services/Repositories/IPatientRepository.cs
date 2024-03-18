using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IPatientRepository : IAsyncRepository<Patient, Guid>, IRepository<Patient, Guid>
{
}

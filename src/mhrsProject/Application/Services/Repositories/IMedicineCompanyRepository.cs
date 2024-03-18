using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IMedicineCompanyRepository : IAsyncRepository<MedicineCompany, Guid>, IRepository<MedicineCompany, Guid>
{
}

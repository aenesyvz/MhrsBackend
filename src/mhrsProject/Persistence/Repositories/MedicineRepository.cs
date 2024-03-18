using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class MedicineRepository : EfRepositoryBase<Medicine, Guid, BaseDbContext>, IMedicineRepository
{
    public MedicineRepository(BaseDbContext context) : base(context)
    {
    }
}

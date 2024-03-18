using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class HospitalRepository : EfRepositoryBase<Hospital, Guid, BaseDbContext>, IHospitalRepository
{
    public HospitalRepository(BaseDbContext context) : base(context)
    {
    }
}

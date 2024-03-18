using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class PolyclinicRepository : EfRepositoryBase<Polyclinic, Guid, BaseDbContext>, IPolyclinicRepository
{
    public PolyclinicRepository(BaseDbContext context) : base(context)
    {
    }
}

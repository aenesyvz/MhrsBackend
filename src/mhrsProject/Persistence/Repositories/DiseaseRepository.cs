using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class DiseaseRepository : EfRepositoryBase<Disease, Guid, BaseDbContext>, IDiseaseRepository
{
    public DiseaseRepository(BaseDbContext context) : base(context)
    {
    }
}

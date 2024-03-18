using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class PrescriptionDetailRepository : EfRepositoryBase<PrescriptionDetail, Guid, BaseDbContext>, IPrescriptionDetailRepository
{
    public PrescriptionDetailRepository(BaseDbContext context) : base(context)
    {
    }
}

using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class PrescriptionRepository : EfRepositoryBase<Prescription, Guid, BaseDbContext>, IPrescriptionRepository
{
    public PrescriptionRepository(BaseDbContext context) : base(context)
    {
    }
}
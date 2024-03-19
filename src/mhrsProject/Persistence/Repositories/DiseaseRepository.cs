using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class DiseaseRepository : EfRepositoryBase<Disease, Guid, BaseDbContext>, IDiseaseRepository
{
    public DiseaseRepository(BaseDbContext context) : base(context)
    {
    }

    public async Task<List<Disease>> GetListByPolyclinicIdAsync(Guid polyclinicId)
    {
        var diseases = await Query().AsNoTracking().Where(d => d.PolyclinicId == polyclinicId).OrderBy(x => x.Name).ToListAsync();
        return diseases;
    }
}

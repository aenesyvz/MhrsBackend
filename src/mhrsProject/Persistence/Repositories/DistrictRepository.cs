using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class DistrictRepository : EfRepositoryBase<District, Guid, BaseDbContext>, IDistrictRepository
{
    public DistrictRepository(BaseDbContext context) : base(context)
    {
    }

    public async Task<List<District>> GetListDistrictByCityIdAsync(Guid cityId)
    {
        var districts = await Query().AsNoTracking().Include(c => c.City).Where(d => d.CityId == cityId).OrderBy(c => c.Name).ToListAsync();
        return districts;
    }
}

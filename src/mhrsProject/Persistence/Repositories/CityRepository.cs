using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class CityRepository : EfRepositoryBase<City, Guid, BaseDbContext>, ICityRepository
{
    public CityRepository(BaseDbContext context) : base(context)
    {
    }

    public async Task<List<City>> GetListWithoutAsync()
    {
        var cities = await Query().AsNoTracking().OrderBy(c => c.PlateCode).ToListAsync();
        return cities;
    }
}

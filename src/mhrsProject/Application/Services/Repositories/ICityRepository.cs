﻿using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ICityRepository : IAsyncRepository<City, Guid>, IRepository<City, Guid>
{
    Task<List<City>> GetListWithoutAsync();
}

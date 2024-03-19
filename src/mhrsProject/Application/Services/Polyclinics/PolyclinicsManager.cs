using Application.Features.Polyclinics.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Polyclinics;

public class PolyclinicsManager : IPolyclinicsService
{
    private readonly IPolyclinicRepository _polyclinicRepository;
    private readonly PolyclinicBusinessRules _polyclinicBusinessRules;

    public PolyclinicsManager(IPolyclinicRepository polyclinicRepository, PolyclinicBusinessRules polyclinicBusinessRules)
    {
        _polyclinicRepository = polyclinicRepository;
        _polyclinicBusinessRules = polyclinicBusinessRules;
    }

    public async Task<Polyclinic?> GetAsync(
        Expression<Func<Polyclinic, bool>> predicate,
        Func<IQueryable<Polyclinic>, IIncludableQueryable<Polyclinic, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Polyclinic? polyclinic = await _polyclinicRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return polyclinic;
    }

    public async Task<IPaginate<Polyclinic>?> GetListAsync(
        Expression<Func<Polyclinic, bool>>? predicate = null,
        Func<IQueryable<Polyclinic>, IOrderedQueryable<Polyclinic>>? orderBy = null,
        Func<IQueryable<Polyclinic>, IIncludableQueryable<Polyclinic, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Polyclinic> polyclinicList = await _polyclinicRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return polyclinicList;
    }

    public async Task<Polyclinic> AddAsync(Polyclinic polyclinic)
    {
        Polyclinic addedPolyclinic = await _polyclinicRepository.AddAsync(polyclinic);

        return addedPolyclinic;
    }

    public async Task<Polyclinic> UpdateAsync(Polyclinic polyclinic)
    {
        Polyclinic updatedPolyclinic = await _polyclinicRepository.UpdateAsync(polyclinic);

        return updatedPolyclinic;
    }

    public async Task<Polyclinic> DeleteAsync(Polyclinic polyclinic, bool permanent = false)
    {
        Polyclinic deletedPolyclinic = await _polyclinicRepository.DeleteAsync(polyclinic);

        return deletedPolyclinic;
    }
}

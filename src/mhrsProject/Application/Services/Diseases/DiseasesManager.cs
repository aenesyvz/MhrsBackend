using Application.Features.Diseases.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Diseases;

public class DiseasesManager : IDiseasesService
{
    private readonly IDiseaseRepository _diseaseRepository;
    private readonly DiseaseBusinessRules _diseaseBusinessRules;

    public DiseasesManager(IDiseaseRepository diseaseRepository, DiseaseBusinessRules diseaseBusinessRules)
    {
        _diseaseRepository = diseaseRepository;
        _diseaseBusinessRules = diseaseBusinessRules;
    }

    public async Task<Disease?> GetAsync(
        Expression<Func<Disease, bool>> predicate,
        Func<IQueryable<Disease>, IIncludableQueryable<Disease, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Disease? disease = await _diseaseRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return disease;
    }

    public async Task<IPaginate<Disease>?> GetListAsync(
        Expression<Func<Disease, bool>>? predicate = null,
        Func<IQueryable<Disease>, IOrderedQueryable<Disease>>? orderBy = null,
        Func<IQueryable<Disease>, IIncludableQueryable<Disease, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Disease> diseaseList = await _diseaseRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return diseaseList;
    }

    public async Task<Disease> AddAsync(Disease disease)
    {
        Disease addedDisease = await _diseaseRepository.AddAsync(disease);

        return addedDisease;
    }

    public async Task<Disease> UpdateAsync(Disease disease)
    {
        Disease updatedDisease = await _diseaseRepository.UpdateAsync(disease);

        return updatedDisease;
    }

    public async Task<Disease> DeleteAsync(Disease disease, bool permanent = false)
    {
        Disease deletedDisease = await _diseaseRepository.DeleteAsync(disease);

        return deletedDisease;
    }
}

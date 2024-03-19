using Application.Features.PrescriptionDetails.Rules;
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

namespace Application.Services.PrescriptionDetails;



public class PrescriptionDetailsManager : IPrescriptionDetailsService
{
    private readonly IPrescriptionDetailRepository _prescriptionDetailRepository;
    private readonly PrescriptionDetailBusinessRules _prescriptionDetailBusinessRules;

    public PrescriptionDetailsManager(IPrescriptionDetailRepository prescriptionDetailRepository, PrescriptionDetailBusinessRules prescriptionDetailBusinessRules)
    {
        _prescriptionDetailRepository = prescriptionDetailRepository;
        _prescriptionDetailBusinessRules = prescriptionDetailBusinessRules;
    }

    public async Task<PrescriptionDetail?> GetAsync(
        Expression<Func<PrescriptionDetail, bool>> predicate,
        Func<IQueryable<PrescriptionDetail>, IIncludableQueryable<PrescriptionDetail, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        PrescriptionDetail? prescriptionDetail = await _prescriptionDetailRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return prescriptionDetail;
    }

    public async Task<IPaginate<PrescriptionDetail>?> GetListAsync(
        Expression<Func<PrescriptionDetail, bool>>? predicate = null,
        Func<IQueryable<PrescriptionDetail>, IOrderedQueryable<PrescriptionDetail>>? orderBy = null,
        Func<IQueryable<PrescriptionDetail>, IIncludableQueryable<PrescriptionDetail, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<PrescriptionDetail> prescriptionDetailList = await _prescriptionDetailRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return prescriptionDetailList;
    }

    public async Task<PrescriptionDetail> AddAsync(PrescriptionDetail prescriptionDetail)
    {
        PrescriptionDetail addedPrescriptionDetail = await _prescriptionDetailRepository.AddAsync(prescriptionDetail);

        return addedPrescriptionDetail;
    }

    public async Task<PrescriptionDetail> UpdateAsync(PrescriptionDetail prescriptionDetail)
    {
        PrescriptionDetail updatedPrescriptionDetail = await _prescriptionDetailRepository.UpdateAsync(prescriptionDetail);

        return updatedPrescriptionDetail;
    }

    public async Task<PrescriptionDetail> DeleteAsync(PrescriptionDetail prescriptionDetail, bool permanent = false)
    {
        PrescriptionDetail deletedPrescriptionDetail = await _prescriptionDetailRepository.DeleteAsync(prescriptionDetail);

        return deletedPrescriptionDetail;
    }
}

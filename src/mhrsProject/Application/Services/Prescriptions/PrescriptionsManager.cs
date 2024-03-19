using Application.Features.Prescriptions.Rules;
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

namespace Application.Services.Prescriptions;

public class PrescriptionsManager : IPrescriptionsService
{
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly PrescriptionBusinessRules _prescriptionBusinessRules;

    public PrescriptionsManager(IPrescriptionRepository prescriptionRepository, PrescriptionBusinessRules prescriptionBusinessRules)
    {
        _prescriptionRepository = prescriptionRepository;
        _prescriptionBusinessRules = prescriptionBusinessRules;
    }

    public async Task<Prescription?> GetAsync(
        Expression<Func<Prescription, bool>> predicate,
        Func<IQueryable<Prescription>, IIncludableQueryable<Prescription, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Prescription? prescription = await _prescriptionRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return prescription;
    }

    public async Task<IPaginate<Prescription>?> GetListAsync(
        Expression<Func<Prescription, bool>>? predicate = null,
        Func<IQueryable<Prescription>, IOrderedQueryable<Prescription>>? orderBy = null,
        Func<IQueryable<Prescription>, IIncludableQueryable<Prescription, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Prescription> prescriptionList = await _prescriptionRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return prescriptionList;
    }

    public async Task<Prescription> AddAsync(Prescription prescription)
    {
        Prescription addedPrescription = await _prescriptionRepository.AddAsync(prescription);

        return addedPrescription;
    }

    public async Task<Prescription> UpdateAsync(Prescription prescription)
    {
        Prescription updatedPrescription = await _prescriptionRepository.UpdateAsync(prescription);

        return updatedPrescription;
    }

    public async Task<Prescription> DeleteAsync(Prescription prescription, bool permanent = false)
    {
        Prescription deletedPrescription = await _prescriptionRepository.DeleteAsync(prescription);

        return deletedPrescription;
    }
}

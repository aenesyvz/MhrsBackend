using Application.Features.Medicines.Rules;
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

namespace Application.Services.Medicines;
public class MedicinesManager : IMedicinesService
{
    private readonly IMedicineRepository _medicineRepository;
    private readonly MedicineBusinessRules _medicineBusinessRules;

    public MedicinesManager(IMedicineRepository medicineRepository, MedicineBusinessRules medicineBusinessRules)
    {
        _medicineRepository = medicineRepository;
        _medicineBusinessRules = medicineBusinessRules;
    }

    public async Task<Medicine?> GetAsync(
        Expression<Func<Medicine, bool>> predicate,
        Func<IQueryable<Medicine>, IIncludableQueryable<Medicine, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Medicine? medicine = await _medicineRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return medicine;
    }

    public async Task<IPaginate<Medicine>?> GetListAsync(
        Expression<Func<Medicine, bool>>? predicate = null,
        Func<IQueryable<Medicine>, IOrderedQueryable<Medicine>>? orderBy = null,
        Func<IQueryable<Medicine>, IIncludableQueryable<Medicine, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Medicine> medicineList = await _medicineRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return medicineList;
    }

   // public async Task<IList<Medicine>> GetListWithoutPaginationAsync(
   //    Expression<Func<Medicine, bool>>? predicate = null,
   //    Func<IQueryable<Medicine>, IOrderedQueryable<Medicine>>? orderBy = null,
   //    Func<IQueryable<Medicine>, IIncludableQueryable<Medicine, object>>? include = null,
   //    bool withDeleted = false,
   //    bool enableTracking = true,
   //    CancellationToken cancellationToken = default
   //)
   // {
   //     IList<Medicine> medicineList = await _medicineRepository.GetListWithoutPaginationAsync(
   //         predicate,
   //         orderBy,
   //         include,
   //         withDeleted,
   //         enableTracking,
   //         cancellationToken
   //     );
   //     return medicineList;
   // }

    public async Task<Medicine> AddAsync(Medicine medicine)
    {
        Medicine addedMedicine = await _medicineRepository.AddAsync(medicine);

        return addedMedicine;
    }

    public async Task<Medicine> UpdateAsync(Medicine medicine)
    {
        Medicine updatedMedicine = await _medicineRepository.UpdateAsync(medicine);

        return updatedMedicine;
    }

    public async Task<Medicine> DeleteAsync(Medicine medicine, bool permanent = false)
    {
        Medicine deletedMedicine = await _medicineRepository.DeleteAsync(medicine);

        return deletedMedicine;
    }
}

using Application.Features.MedicineCompanies.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.MedicineCompanies;

public class MedicineCompaniesManager : IMedicineCompaniesService
{
    private readonly IMedicineCompanyRepository _medicineCompanyRepository;
    private readonly MedicineCompanyBusinessRules _medicineCompanyBusinessRules;

    public MedicineCompaniesManager(IMedicineCompanyRepository medicineCompanyRepository, MedicineCompanyBusinessRules medicineCompanyBusinessRules)
    {
        _medicineCompanyRepository = medicineCompanyRepository;
        _medicineCompanyBusinessRules = medicineCompanyBusinessRules;
    }

    public async Task<MedicineCompany?> GetAsync(
        Expression<Func<MedicineCompany, bool>> predicate,
        Func<IQueryable<MedicineCompany>, IIncludableQueryable<MedicineCompany, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        MedicineCompany? medicineCompany = await _medicineCompanyRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return medicineCompany;
    }

    public async Task<IPaginate<MedicineCompany>?> GetListAsync(
        Expression<Func<MedicineCompany, bool>>? predicate = null,
        Func<IQueryable<MedicineCompany>, IOrderedQueryable<MedicineCompany>>? orderBy = null,
        Func<IQueryable<MedicineCompany>, IIncludableQueryable<MedicineCompany, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<MedicineCompany> medicineCompanyList = await _medicineCompanyRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return medicineCompanyList;
    }

    public async Task<MedicineCompany> AddAsync(MedicineCompany medicineCompany)
    {
        MedicineCompany addedMedicineCompany = await _medicineCompanyRepository.AddAsync(medicineCompany);

        return addedMedicineCompany;
    }

    public async Task<MedicineCompany> UpdateAsync(MedicineCompany medicineCompany)
    {
        MedicineCompany updatedMedicineCompany = await _medicineCompanyRepository.UpdateAsync(medicineCompany);

        return updatedMedicineCompany;
    }

    public async Task<MedicineCompany> DeleteAsync(MedicineCompany medicineCompany, bool permanent = false)
    {
        MedicineCompany deletedMedicineCompany = await _medicineCompanyRepository.DeleteAsync(medicineCompany);

        return deletedMedicineCompany;
    }
}

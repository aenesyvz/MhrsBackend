using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.MedicineCompanies;
public interface IMedicineCompaniesService
{
    Task<MedicineCompany?> GetAsync(
        Expression<Func<MedicineCompany, bool>> predicate,
        Func<IQueryable<MedicineCompany>, IIncludableQueryable<MedicineCompany, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<MedicineCompany>?> GetListAsync(
        Expression<Func<MedicineCompany, bool>>? predicate = null,
        Func<IQueryable<MedicineCompany>, IOrderedQueryable<MedicineCompany>>? orderBy = null,
        Func<IQueryable<MedicineCompany>, IIncludableQueryable<MedicineCompany, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<MedicineCompany> AddAsync(MedicineCompany medicineCompany);
    Task<MedicineCompany> UpdateAsync(MedicineCompany medicineCompany);
    Task<MedicineCompany> DeleteAsync(MedicineCompany medicineCompany, bool permanent = false);
}

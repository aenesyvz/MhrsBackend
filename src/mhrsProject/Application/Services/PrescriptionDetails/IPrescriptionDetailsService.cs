using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.PrescriptionDetails;

public interface IPrescriptionDetailsService
{
    Task<PrescriptionDetail?> GetAsync(
        Expression<Func<PrescriptionDetail, bool>> predicate,
        Func<IQueryable<PrescriptionDetail>, IIncludableQueryable<PrescriptionDetail, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<PrescriptionDetail>?> GetListAsync(
        Expression<Func<PrescriptionDetail, bool>>? predicate = null,
        Func<IQueryable<PrescriptionDetail>, IOrderedQueryable<PrescriptionDetail>>? orderBy = null,
        Func<IQueryable<PrescriptionDetail>, IIncludableQueryable<PrescriptionDetail, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<PrescriptionDetail> AddAsync(PrescriptionDetail prescriptionDetail);
    Task<PrescriptionDetail> UpdateAsync(PrescriptionDetail prescriptionDetail);
    Task<PrescriptionDetail> DeleteAsync(PrescriptionDetail prescriptionDetail, bool permanent = false);
}

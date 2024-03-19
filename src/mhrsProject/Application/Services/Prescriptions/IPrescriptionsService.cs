using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Prescriptions;

public interface IPrescriptionsService
{
    Task<Prescription?> GetAsync(
        Expression<Func<Prescription, bool>> predicate,
        Func<IQueryable<Prescription>, IIncludableQueryable<Prescription, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Prescription>?> GetListAsync(
        Expression<Func<Prescription, bool>>? predicate = null,
        Func<IQueryable<Prescription>, IOrderedQueryable<Prescription>>? orderBy = null,
        Func<IQueryable<Prescription>, IIncludableQueryable<Prescription, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Prescription> AddAsync(Prescription prescription);
    Task<Prescription> UpdateAsync(Prescription prescription);
    Task<Prescription> DeleteAsync(Prescription prescription, bool permanent = false);
}

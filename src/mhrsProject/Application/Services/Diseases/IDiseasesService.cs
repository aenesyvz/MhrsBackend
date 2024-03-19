using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Diseases;

public interface IDiseasesService
{
    Task<Disease?> GetAsync(
        Expression<Func<Disease, bool>> predicate,
        Func<IQueryable<Disease>, IIncludableQueryable<Disease, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Disease>?> GetListAsync(
        Expression<Func<Disease, bool>>? predicate = null,
        Func<IQueryable<Disease>, IOrderedQueryable<Disease>>? orderBy = null,
        Func<IQueryable<Disease>, IIncludableQueryable<Disease, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Disease> AddAsync(Disease disease);
    Task<Disease> UpdateAsync(Disease disease);
    Task<Disease> DeleteAsync(Disease disease, bool permanent = false);
}

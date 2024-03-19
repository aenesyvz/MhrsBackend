using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Medicines;

public interface IMedicinesService
{
    Task<Medicine?> GetAsync(
        Expression<Func<Medicine, bool>> predicate,
        Func<IQueryable<Medicine>, IIncludableQueryable<Medicine, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Medicine>?> GetListAsync(
        Expression<Func<Medicine, bool>>? predicate = null,
        Func<IQueryable<Medicine>, IOrderedQueryable<Medicine>>? orderBy = null,
        Func<IQueryable<Medicine>, IIncludableQueryable<Medicine, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

   // Task<IList<Medicine>> GetListWithoutPaginationAsync(
   //    Expression<Func<Medicine, bool>>? predicate = null,
   //    Func<IQueryable<Medicine>, IOrderedQueryable<Medicine>>? orderBy = null,
   //    Func<IQueryable<Medicine>, IIncludableQueryable<Medicine, object>>? include = null,
   //    bool withDeleted = false,
   //    bool enableTracking = true,
   //    CancellationToken cancellationToken = default
   //);
    Task<Medicine> AddAsync(Medicine medicine);
    Task<Medicine> UpdateAsync(Medicine medicine);
    Task<Medicine> DeleteAsync(Medicine medicine, bool permanent = false);
}

using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Polyclinics;
public interface IPolyclinicsService
{
    Task<Polyclinic?> GetAsync(
        Expression<Func<Polyclinic, bool>> predicate,
        Func<IQueryable<Polyclinic>, IIncludableQueryable<Polyclinic, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Polyclinic>?> GetListAsync(
        Expression<Func<Polyclinic, bool>>? predicate = null,
        Func<IQueryable<Polyclinic>, IOrderedQueryable<Polyclinic>>? orderBy = null,
        Func<IQueryable<Polyclinic>, IIncludableQueryable<Polyclinic, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Polyclinic> AddAsync(Polyclinic polyclinic);
    Task<Polyclinic> UpdateAsync(Polyclinic polyclinic);
    Task<Polyclinic> DeleteAsync(Polyclinic polyclinic, bool permanent = false);
}

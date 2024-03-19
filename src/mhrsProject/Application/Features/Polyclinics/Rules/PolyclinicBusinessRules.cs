using Application.Features.Polyclinics.Contants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Polyclinics.Rules;
public class PolyclinicBusinessRules : BaseBusinessRules
{
    private readonly IPolyclinicRepository _polyclinicRepository;

    public PolyclinicBusinessRules(IPolyclinicRepository polyclinicRepository)
    {
        _polyclinicRepository = polyclinicRepository;
    }

    public Task PolyclinicShouldExistWhenSelected(Polyclinic? polyclinic)
    {
        if (polyclinic == null)
            throw new BusinessException(PolyclinicsBusinessMessages.PolyclinicNotExists);
        return Task.CompletedTask;
    }

    public async Task PolyclinicIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Polyclinic? polyclinic = await _polyclinicRepository.GetAsync(
            predicate: p => p.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await PolyclinicShouldExistWhenSelected(polyclinic);
    }
}
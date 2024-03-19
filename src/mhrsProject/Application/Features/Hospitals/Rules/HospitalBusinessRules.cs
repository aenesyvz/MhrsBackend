using Application.Features.Hospitals.Contants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hospitals.Rules;
public class HospitalBusinessRules : BaseBusinessRules
{
    private readonly IHospitalRepository _hospitalRepository;

    public HospitalBusinessRules(IHospitalRepository hospitalRepository)
    {
        _hospitalRepository = hospitalRepository;
    }

    public Task HospitalShouldExistWhenSelected(Hospital? hospital)
    {
        if (hospital == null)
            throw new BusinessException(HospitalsBusinessMessages.HospitalNotExists);
        return Task.CompletedTask;
    }

    public async Task HospitalIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Hospital? hospital = await _hospitalRepository.GetAsync(
            predicate: h => h.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await HospitalShouldExistWhenSelected(hospital);
    }
}
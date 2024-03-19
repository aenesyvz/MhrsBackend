using Application.Features.Medicines.Contants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Medicines.Rules;
public class MedicineBusinessRules : BaseBusinessRules
{
    private readonly IMedicineRepository _medicineRepository;

    public MedicineBusinessRules(IMedicineRepository medicineRepository)
    {
        _medicineRepository = medicineRepository;
    }

    public Task MedicineShouldExistWhenSelected(Medicine? medicine)
    {
        if (medicine == null)
            throw new BusinessException(MedicinesBusinessMessages.MedicineNotExists);
        return Task.CompletedTask;
    }

    public async Task MedicineIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Medicine? medicine = await _medicineRepository.GetAsync(
            predicate: m => m.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await MedicineShouldExistWhenSelected(medicine);
    }
}
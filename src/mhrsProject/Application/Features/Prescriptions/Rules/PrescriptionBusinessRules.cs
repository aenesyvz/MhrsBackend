using Application.Features.Prescriptions.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Prescriptions.Rules;
public class PrescriptionBusinessRules : BaseBusinessRules
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public PrescriptionBusinessRules(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }

    public Task PrescriptionShouldExistWhenSelected(Prescription? prescription)
    {
        if (prescription == null)
            throw new BusinessException(PrescriptionsBusinessMessages.PrescriptionNotExists);
        return Task.CompletedTask;
    }

    public async Task PrescriptionIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Prescription? prescription = await _prescriptionRepository.GetAsync(
            predicate: p => p.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await PrescriptionShouldExistWhenSelected(prescription);
    }
}
using Application.Features.PrescriptionDetails.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PrescriptionDetails.Rules;
public class PrescriptionDetailBusinessRules : BaseBusinessRules
{
    private readonly IPrescriptionDetailRepository _prescriptionDetailRepository;

    public PrescriptionDetailBusinessRules(IPrescriptionDetailRepository prescriptionDetailRepository)
    {
        _prescriptionDetailRepository = prescriptionDetailRepository;
    }

    public Task PrescriptionDetailShouldExistWhenSelected(PrescriptionDetail? prescriptionDetail)
    {
        if (prescriptionDetail == null)
            throw new BusinessException(PrescriptionDetailsBusinessMessages.PrescriptionDetailNotExists);
        return Task.CompletedTask;
    }

    public async Task PrescriptionDetailIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        PrescriptionDetail? prescriptionDetail = await _prescriptionDetailRepository.GetAsync(
            predicate: pd => pd.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await PrescriptionDetailShouldExistWhenSelected(prescriptionDetail);
    }
}
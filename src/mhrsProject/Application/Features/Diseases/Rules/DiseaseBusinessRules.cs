using Application.Features.Diseases.Contants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Diseases.Rules;
public class DiseaseBusinessRules : BaseBusinessRules
{
    private readonly IDiseaseRepository _diseaseRepository;

    public DiseaseBusinessRules(IDiseaseRepository diseaseRepository)
    {
        _diseaseRepository = diseaseRepository;
    }

    public Task DiseaseShouldExistWhenSelected(Disease? disease)
    {
        if (disease == null)
            throw new BusinessException(DiseasesBusinessMessages.DiseaseNotExists);
        return Task.CompletedTask;
    }

    public async Task DiseaseIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Disease? disease = await _diseaseRepository.GetAsync(
            predicate: d => d.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await DiseaseShouldExistWhenSelected(disease);
    }

    public async Task DiseaseCannotBeDuplicateWhenInsertedOrUpdated(Guid polyclinicId, string name)
    {
        Disease? disease = await _diseaseRepository.GetAsync(
                predicate: d => d.PolyclinicId == polyclinicId && d.Name.ToLower() == name.ToLower()
            );

        if (disease != null)
        {
            throw new BusinessException(DiseasesBusinessMessages.DiseaseExistsInThisPolyclinic);
        }
    }
}
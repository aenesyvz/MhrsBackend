using Application.Features.Districts.Contants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Districts.Rules;
public class DistrictBusinessRules : BaseBusinessRules
{
    private readonly IDistrictRepository _districtRepository;

    public DistrictBusinessRules(IDistrictRepository districtRepository)
    {
        _districtRepository = districtRepository;
    }

    public Task DistrictShouldExistWhenSelected(District? district)
    {
        if (district == null)
            throw new BusinessException(DistrictsBusinessMessages.DistrictNotExists);
        return Task.CompletedTask;
    }

    public async Task DistrictIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        District? district = await _districtRepository.GetAsync(
            predicate: d => d.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await DistrictShouldExistWhenSelected(district);
    }

    public async Task DistrictNameCannotBeDuplicateWhenInCityInsertedOrUpdated(Guid cityId, string districtName)
    {
        District? district = await _districtRepository.GetAsync(
                predicate: d => d.CityId == cityId && d.Name.ToLower() == districtName.ToLower()
            );

        if (district != null)
        {
            throw new BusinessException(DistrictsBusinessMessages.DistrictExistsInCity);
        }
    }
}
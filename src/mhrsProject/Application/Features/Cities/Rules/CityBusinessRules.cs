using Application.Features.Cities.Contants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cities.Rules;
public class CityBusinessRules : BaseBusinessRules
{
    private readonly ICityRepository _cityRepository;

    public CityBusinessRules(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public Task CityShouldExistWhenSelected(City? city)
    {
        if (city == null)
            throw new BusinessException(CitiesBusinessMessages.CityNotExists);
        return Task.CompletedTask;
    }

    public async Task CityIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        City? city = await _cityRepository.GetAsync(
            predicate: c => c.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await CityShouldExistWhenSelected(city);
    }

    public async Task CityNameCannotBeDuplicatedWhenInsertedOrUpdated(string cityName)
    {
        City? city = await _cityRepository.GetAsync(
                predicate: c => c.Name.ToLower() == cityName.ToLower()
            );
        if (city != null)
        {
            throw new BusinessException(CitiesBusinessMessages.CityNameExists);
        }
    }
}
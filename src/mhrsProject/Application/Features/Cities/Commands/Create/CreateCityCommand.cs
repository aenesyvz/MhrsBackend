using Application.Features.Cities.Contants;
using Application.Features.Cities.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Cities.Contants.CitiesOperationClaims;

namespace Application.Features.Cities.Commands.Create;

public class CreateCityCommand : IRequest<CreatedCityResponse>,/* ISecuredRequest, */ICacheRemoverRequest, ITransactionalRequest
{
    public int PlateCode { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, CitiesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => new[] { "GetCities" };

    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, CreatedCityResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepository;
        private readonly CityBusinessRules _cityBusinessRules;

        public CreateCityCommandHandler(IMapper mapper, ICityRepository cityRepository,
                                         CityBusinessRules cityBusinessRules)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
            _cityBusinessRules = cityBusinessRules;
        }

        public async Task<CreatedCityResponse> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            await _cityBusinessRules.CityNameCannotBeDuplicatedWhenInsertedOrUpdated(request.Name);

            City city = _mapper.Map<City>(request);

            await _cityRepository.AddAsync(city);

            CreatedCityResponse response = _mapper.Map<CreatedCityResponse>(city);
            return response;
        }
    }
}
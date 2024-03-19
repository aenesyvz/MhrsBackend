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

namespace Application.Features.Cities.Commands.Delete;


public class DeleteCityCommand : IRequest<DeletedCityResponse>,/* ISecuredRequest,*/ ICacheRemoverRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, CitiesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetCities" };

    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand, DeletedCityResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepository;
        private readonly CityBusinessRules _cityBusinessRules;

        public DeleteCityCommandHandler(IMapper mapper, ICityRepository cityRepository,
                                         CityBusinessRules cityBusinessRules)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
            _cityBusinessRules = cityBusinessRules;
        }

        public async Task<DeletedCityResponse> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            City? city = await _cityRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            await _cityBusinessRules.CityShouldExistWhenSelected(city);

            await _cityRepository.DeleteAsync(city!);

            DeletedCityResponse response = _mapper.Map<DeletedCityResponse>(city);
            return response;
        }
    }
}
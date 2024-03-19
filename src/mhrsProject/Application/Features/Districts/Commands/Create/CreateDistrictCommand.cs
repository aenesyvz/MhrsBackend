using Application.Features.Districts.Contants;
using Application.Features.Districts.Rules;
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
using static Application.Features.Districts.Contants.DistrictsOperationClaims;

namespace Application.Features.Districts.Commands.Create;

public class CreateDistrictCommand : IRequest<CreatedDistrictResponse>,/* ISecuredRequest,*/ ICacheRemoverRequest, ITransactionalRequest
{
    public Guid CityId { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, DistrictsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetDistricts" };

    public class CreateDistrictCommandHandler : IRequestHandler<CreateDistrictCommand, CreatedDistrictResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDistrictRepository _districtRepository;
        private readonly DistrictBusinessRules _districtBusinessRules;

        public CreateDistrictCommandHandler(IMapper mapper, IDistrictRepository districtRepository,
                                         DistrictBusinessRules districtBusinessRules)
        {
            _mapper = mapper;
            _districtRepository = districtRepository;
            _districtBusinessRules = districtBusinessRules;
        }

        public async Task<CreatedDistrictResponse> Handle(CreateDistrictCommand request, CancellationToken cancellationToken)
        {
            await _districtBusinessRules.DistrictNameCannotBeDuplicateWhenInCityInsertedOrUpdated(request.CityId, request.Name);

            District district = _mapper.Map<District>(request);

            await _districtRepository.AddAsync(district);

            CreatedDistrictResponse response = _mapper.Map<CreatedDistrictResponse>(district);
            return response;
        }
    }
}
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

namespace Application.Features.Districts.Commands.Delete;

public class DeleteDistrictCommand : IRequest<DeletedDistrictResponse>,/* ISecuredRequest,*/ ICacheRemoverRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, DistrictsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey =>new[] { "GetDistricts" };

    public class DeleteDistrictCommandHandler : IRequestHandler<DeleteDistrictCommand, DeletedDistrictResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDistrictRepository _districtRepository;
        private readonly DistrictBusinessRules _districtBusinessRules;

        public DeleteDistrictCommandHandler(IMapper mapper, IDistrictRepository districtRepository,
                                         DistrictBusinessRules districtBusinessRules)
        {
            _mapper = mapper;
            _districtRepository = districtRepository;
            _districtBusinessRules = districtBusinessRules;
        }

        public async Task<DeletedDistrictResponse> Handle(DeleteDistrictCommand request, CancellationToken cancellationToken)
        {
            District? district = await _districtRepository.GetAsync(predicate: d => d.Id == request.Id, cancellationToken: cancellationToken);
            await _districtBusinessRules.DistrictShouldExistWhenSelected(district);

            await _districtRepository.DeleteAsync(district!);

            DeletedDistrictResponse response = _mapper.Map<DeletedDistrictResponse>(district);
            return response;
        }
    }
}
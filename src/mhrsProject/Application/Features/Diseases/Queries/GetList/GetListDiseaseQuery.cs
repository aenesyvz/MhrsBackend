using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Diseases.Contants.DiseasesOperationClaims;

namespace Application.Features.Diseases.Queries.GetList;

public class GetListDiseaseQuery : IRequest<GetListResponse<GetListDiseaseListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListDiseases({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetDiseases";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListDiseaseQueryHandler : IRequestHandler<GetListDiseaseQuery, GetListResponse<GetListDiseaseListItemDto>>
    {
        private readonly IDiseaseRepository _diseaseRepository;
        private readonly IMapper _mapper;

        public GetListDiseaseQueryHandler(IDiseaseRepository diseaseRepository, IMapper mapper)
        {
            _diseaseRepository = diseaseRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListDiseaseListItemDto>> Handle(GetListDiseaseQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Disease> diseases = await _diseaseRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListDiseaseListItemDto> response = _mapper.Map<GetListResponse<GetListDiseaseListItemDto>>(diseases);
            return response;
        }
    }
}
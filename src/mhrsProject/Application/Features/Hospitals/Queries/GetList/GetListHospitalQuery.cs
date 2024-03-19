using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Hospitals.Contants.HospitalsOperationClaims;

namespace Application.Features.Hospitals.Queries.GetList;


public class GetListHospitalQuery : IRequest<GetListResponse<GetListHospitalListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListHospitals({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetHospitals";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListHospitalQueryHandler : IRequestHandler<GetListHospitalQuery, GetListResponse<GetListHospitalListItemDto>>
    {
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IMapper _mapper;

        public GetListHospitalQueryHandler(IHospitalRepository hospitalRepository, IMapper mapper)
        {
            _hospitalRepository = hospitalRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListHospitalListItemDto>> Handle(GetListHospitalQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Hospital> hospitals = await _hospitalRepository.GetListAsync(
                include: h => h.Include(h => h.City).Include(h => h.District),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListHospitalListItemDto> response = _mapper.Map<GetListResponse<GetListHospitalListItemDto>>(hospitals);
            return response;
        }
    }
}
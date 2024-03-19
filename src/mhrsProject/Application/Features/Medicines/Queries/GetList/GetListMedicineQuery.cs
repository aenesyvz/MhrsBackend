using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
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
using static Application.Features.Medicines.Contants.MedicinesOperationClaims;

namespace Application.Features.Medicines.Queries.GetList;

public class GetListMedicineQuery : IRequest<GetListResponse<GetListMedicineListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListMedicines({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetMedicines";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListMedicineQueryHandler : IRequestHandler<GetListMedicineQuery, GetListResponse<GetListMedicineListItemDto>>
    {
        private readonly IMedicineRepository _medicineRepository;
        private readonly IMapper _mapper;

        public GetListMedicineQueryHandler(IMedicineRepository medicineRepository, IMapper mapper)
        {
            _medicineRepository = medicineRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListMedicineListItemDto>> Handle(GetListMedicineQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Medicine> medicines = await _medicineRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListMedicineListItemDto> response = _mapper.Map<GetListResponse<GetListMedicineListItemDto>>(medicines);
            return response;
        }
    }
}
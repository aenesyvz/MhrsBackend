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
using static Application.Features.PrescriptionDetails.Constants.PrescriptionDetailsOperationClaims;

namespace Application.Features.PrescriptionDetails.Queries.GetList;

public class GetListPrescriptionDetailQuery : IRequest<GetListResponse<GetListPrescriptionDetailListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListPrescriptionDetails({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetPrescriptionDetails";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListPrescriptionDetailQueryHandler : IRequestHandler<GetListPrescriptionDetailQuery, GetListResponse<GetListPrescriptionDetailListItemDto>>
    {
        private readonly IPrescriptionDetailRepository _prescriptionDetailRepository;
        private readonly IMapper _mapper;

        public GetListPrescriptionDetailQueryHandler(IPrescriptionDetailRepository prescriptionDetailRepository, IMapper mapper)
        {
            _prescriptionDetailRepository = prescriptionDetailRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListPrescriptionDetailListItemDto>> Handle(GetListPrescriptionDetailQuery request, CancellationToken cancellationToken)
        {
            IPaginate<PrescriptionDetail> prescriptionDetails = await _prescriptionDetailRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListPrescriptionDetailListItemDto> response = _mapper.Map<GetListResponse<GetListPrescriptionDetailListItemDto>>(prescriptionDetails);
            return response;
        }
    }
}
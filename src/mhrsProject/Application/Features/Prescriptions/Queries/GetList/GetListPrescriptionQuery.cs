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
using static Application.Features.Prescriptions.Constants.PrescriptionsOperationClaims;

namespace Application.Features.Prescriptions.Queries.GetList;


public class GetListPrescriptionQuery : IRequest<GetListResponse<GetListPrescriptionListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListPrescriptions({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetPrescriptions";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListPrescriptionQueryHandler : IRequestHandler<GetListPrescriptionQuery, GetListResponse<GetListPrescriptionListItemDto>>
    {
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IMapper _mapper;

        public GetListPrescriptionQueryHandler(IPrescriptionRepository prescriptionRepository, IMapper mapper)
        {
            _prescriptionRepository = prescriptionRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListPrescriptionListItemDto>> Handle(GetListPrescriptionQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Prescription> prescriptions = await _prescriptionRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListPrescriptionListItemDto> response = _mapper.Map<GetListResponse<GetListPrescriptionListItemDto>>(prescriptions);
            return response;
        }
    }
}
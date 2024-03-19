using Application.Features.PrescriptionDetails.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.PrescriptionDetails.Constants.PrescriptionDetailsOperationClaims;

namespace Application.Features.PrescriptionDetails.Queries.GetById;

public class GetByIdPrescriptionDetailQuery : IRequest<GetByIdPrescriptionDetailResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdPrescriptionDetailQueryHandler : IRequestHandler<GetByIdPrescriptionDetailQuery, GetByIdPrescriptionDetailResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPrescriptionDetailRepository _prescriptionDetailRepository;
        private readonly PrescriptionDetailBusinessRules _prescriptionDetailBusinessRules;

        public GetByIdPrescriptionDetailQueryHandler(IMapper mapper, IPrescriptionDetailRepository prescriptionDetailRepository, PrescriptionDetailBusinessRules prescriptionDetailBusinessRules)
        {
            _mapper = mapper;
            _prescriptionDetailRepository = prescriptionDetailRepository;
            _prescriptionDetailBusinessRules = prescriptionDetailBusinessRules;
        }

        public async Task<GetByIdPrescriptionDetailResponse> Handle(GetByIdPrescriptionDetailQuery request, CancellationToken cancellationToken)
        {
            PrescriptionDetail? prescriptionDetail = await _prescriptionDetailRepository.GetAsync(predicate: pd => pd.Id == request.Id, cancellationToken: cancellationToken);
            await _prescriptionDetailBusinessRules.PrescriptionDetailShouldExistWhenSelected(prescriptionDetail);

            GetByIdPrescriptionDetailResponse response = _mapper.Map<GetByIdPrescriptionDetailResponse>(prescriptionDetail);
            return response;
        }
    }
}

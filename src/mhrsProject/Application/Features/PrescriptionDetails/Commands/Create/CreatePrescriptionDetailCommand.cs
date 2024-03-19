using Application.Features.PrescriptionDetails.Constants;
using Application.Features.PrescriptionDetails.Rules;
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
using static Application.Features.PrescriptionDetails.Constants.PrescriptionDetailsOperationClaims;

namespace Application.Features.PrescriptionDetails.Commands.Create;

public class CreatePrescriptionDetailCommand : IRequest<CreatedPrescriptionDetailResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid PrescriptionId { get; set; }
    public Guid MedicineId { get; set; }
    public string Description { get; set; }
    public int Dose { get; set; }
    public int Period { get; set; }
    public string UsageType { get; set; }
    public int UsageCount { get; set; }

    public string[] Roles => new[] { Admin, Write, PrescriptionDetailsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetPrescriptionDetails" };

    public class CreatePrescriptionDetailCommandHandler : IRequestHandler<CreatePrescriptionDetailCommand, CreatedPrescriptionDetailResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPrescriptionDetailRepository _prescriptionDetailRepository;
        private readonly PrescriptionDetailBusinessRules _prescriptionDetailBusinessRules;

        public CreatePrescriptionDetailCommandHandler(IMapper mapper, IPrescriptionDetailRepository prescriptionDetailRepository,
                                         PrescriptionDetailBusinessRules prescriptionDetailBusinessRules)
        {
            _mapper = mapper;
            _prescriptionDetailRepository = prescriptionDetailRepository;
            _prescriptionDetailBusinessRules = prescriptionDetailBusinessRules;
        }

        public async Task<CreatedPrescriptionDetailResponse> Handle(CreatePrescriptionDetailCommand request, CancellationToken cancellationToken)
        {
            PrescriptionDetail prescriptionDetail = _mapper.Map<PrescriptionDetail>(request);

            await _prescriptionDetailRepository.AddAsync(prescriptionDetail);

            CreatedPrescriptionDetailResponse response = _mapper.Map<CreatedPrescriptionDetailResponse>(prescriptionDetail);
            return response;
        }
    }
}
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

namespace Application.Features.PrescriptionDetails.Commands.Delete;



public class DeletePrescriptionDetailCommand : IRequest<DeletedPrescriptionDetailResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, PrescriptionDetailsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetPrescriptionDetails" };

    public class DeletePrescriptionDetailCommandHandler : IRequestHandler<DeletePrescriptionDetailCommand, DeletedPrescriptionDetailResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPrescriptionDetailRepository _prescriptionDetailRepository;
        private readonly PrescriptionDetailBusinessRules _prescriptionDetailBusinessRules;

        public DeletePrescriptionDetailCommandHandler(IMapper mapper, IPrescriptionDetailRepository prescriptionDetailRepository,
                                         PrescriptionDetailBusinessRules prescriptionDetailBusinessRules)
        {
            _mapper = mapper;
            _prescriptionDetailRepository = prescriptionDetailRepository;
            _prescriptionDetailBusinessRules = prescriptionDetailBusinessRules;
        }

        public async Task<DeletedPrescriptionDetailResponse> Handle(DeletePrescriptionDetailCommand request, CancellationToken cancellationToken)
        {
            PrescriptionDetail? prescriptionDetail = await _prescriptionDetailRepository.GetAsync(predicate: pd => pd.Id == request.Id, cancellationToken: cancellationToken);
            await _prescriptionDetailBusinessRules.PrescriptionDetailShouldExistWhenSelected(prescriptionDetail);

            await _prescriptionDetailRepository.DeleteAsync(prescriptionDetail!);

            DeletedPrescriptionDetailResponse response = _mapper.Map<DeletedPrescriptionDetailResponse>(prescriptionDetail);
            return response;
        }
    }
}

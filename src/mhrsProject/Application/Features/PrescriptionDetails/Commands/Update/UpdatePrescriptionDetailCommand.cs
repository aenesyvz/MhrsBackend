using Application.Features.PrescriptionDetails.Constants;
using Application.Features.PrescriptionDetails.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using static Application.Features.PrescriptionDetails.Constants.PrescriptionDetailsOperationClaims;

namespace Application.Features.PrescriptionDetails.Commands.Update;

public class UpdatePrescriptionDetailCommand : IRequest<UpdatedPrescriptionDetailResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid PrescriptionId { get; set; }
    public Guid MedicineId { get; set; }
    public string Description { get; set; }
    public int Dose { get; set; }
    public int Period { get; set; }
    public string UsageType { get; set; }
    public int UsageCount { get; set; }

    public string[] Roles => new[] { Admin, Write, PrescriptionDetailsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetPrescriptionDetails" };

    public class UpdatePrescriptionDetailCommandHandler : IRequestHandler<UpdatePrescriptionDetailCommand, UpdatedPrescriptionDetailResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPrescriptionDetailRepository _prescriptionDetailRepository;
        private readonly PrescriptionDetailBusinessRules _prescriptionDetailBusinessRules;

        public UpdatePrescriptionDetailCommandHandler(IMapper mapper, IPrescriptionDetailRepository prescriptionDetailRepository,
                                         PrescriptionDetailBusinessRules prescriptionDetailBusinessRules)
        {
            _mapper = mapper;
            _prescriptionDetailRepository = prescriptionDetailRepository;
            _prescriptionDetailBusinessRules = prescriptionDetailBusinessRules;
        }

        public async Task<UpdatedPrescriptionDetailResponse> Handle(UpdatePrescriptionDetailCommand request, CancellationToken cancellationToken)
        {
            PrescriptionDetail? prescriptionDetail = await _prescriptionDetailRepository.GetAsync(predicate: pd => pd.Id == request.Id, cancellationToken: cancellationToken);
            await _prescriptionDetailBusinessRules.PrescriptionDetailShouldExistWhenSelected(prescriptionDetail);
            prescriptionDetail = _mapper.Map(request, prescriptionDetail);

            await _prescriptionDetailRepository.UpdateAsync(prescriptionDetail!);

            UpdatedPrescriptionDetailResponse response = _mapper.Map<UpdatedPrescriptionDetailResponse>(prescriptionDetail);
            return response;
        }
    }
}
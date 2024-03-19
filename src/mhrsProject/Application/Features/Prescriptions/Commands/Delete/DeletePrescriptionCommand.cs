using Application.Features.Prescriptions.Constants;
using Application.Features.Prescriptions.Rules;
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
using static Application.Features.Prescriptions.Constants.PrescriptionsOperationClaims;

namespace Application.Features.Prescriptions.Commands.Delete;



public class DeletePrescriptionCommand : IRequest<DeletedPrescriptionResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, PrescriptionsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetPrescriptions" };

    public class DeletePrescriptionCommandHandler : IRequestHandler<DeletePrescriptionCommand, DeletedPrescriptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly PrescriptionBusinessRules _prescriptionBusinessRules;

        public DeletePrescriptionCommandHandler(IMapper mapper, IPrescriptionRepository prescriptionRepository,
                                         PrescriptionBusinessRules prescriptionBusinessRules)
        {
            _mapper = mapper;
            _prescriptionRepository = prescriptionRepository;
            _prescriptionBusinessRules = prescriptionBusinessRules;
        }

        public async Task<DeletedPrescriptionResponse> Handle(DeletePrescriptionCommand request, CancellationToken cancellationToken)
        {
            Prescription? prescription = await _prescriptionRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _prescriptionBusinessRules.PrescriptionShouldExistWhenSelected(prescription);

            await _prescriptionRepository.DeleteAsync(prescription!);

            DeletedPrescriptionResponse response = _mapper.Map<DeletedPrescriptionResponse>(prescription);
            return response;
        }
    }
}
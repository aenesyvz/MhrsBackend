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

namespace Application.Features.Prescriptions.Commands.Update;

public class UpdatePrescriptionCommand : IRequest<UpdatedPrescriptionResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid HospitalId { get; set; }
    public Guid DoctorId { get; set; }
    public string PrescriptionType { get; set; }

    public string[] Roles => new[] { Admin, Write, PrescriptionsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetPrescriptions" };

    public class UpdatePrescriptionCommandHandler : IRequestHandler<UpdatePrescriptionCommand, UpdatedPrescriptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly PrescriptionBusinessRules _prescriptionBusinessRules;

        public UpdatePrescriptionCommandHandler(IMapper mapper, IPrescriptionRepository prescriptionRepository,
                                         PrescriptionBusinessRules prescriptionBusinessRules)
        {
            _mapper = mapper;
            _prescriptionRepository = prescriptionRepository;
            _prescriptionBusinessRules = prescriptionBusinessRules;
        }

        public async Task<UpdatedPrescriptionResponse> Handle(UpdatePrescriptionCommand request, CancellationToken cancellationToken)
        {
            Prescription? prescription = await _prescriptionRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _prescriptionBusinessRules.PrescriptionShouldExistWhenSelected(prescription);
            prescription = _mapper.Map(request, prescription);

            await _prescriptionRepository.UpdateAsync(prescription!);

            UpdatedPrescriptionResponse response = _mapper.Map<UpdatedPrescriptionResponse>(prescription);
            return response;
        }
    }
}
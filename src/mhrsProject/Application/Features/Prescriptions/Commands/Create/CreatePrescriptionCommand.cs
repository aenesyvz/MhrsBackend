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

namespace Application.Features.Prescriptions.Commands.Create;


public class CreatePrescriptionCommand : IRequest<CreatedPrescriptionResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid PatientId { get; set; }
    public Guid HospitalId { get; set; }
    public Guid DoctorId { get; set; }
    public string PrescriptionType { get; set; }

    public string[] Roles => new[] { Admin, Write, PrescriptionsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetPrescriptions" };

    public class CreatePrescriptionCommandHandler : IRequestHandler<CreatePrescriptionCommand, CreatedPrescriptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly PrescriptionBusinessRules _prescriptionBusinessRules;

        public CreatePrescriptionCommandHandler(IMapper mapper, IPrescriptionRepository prescriptionRepository,
                                         PrescriptionBusinessRules prescriptionBusinessRules)
        {
            _mapper = mapper;
            _prescriptionRepository = prescriptionRepository;
            _prescriptionBusinessRules = prescriptionBusinessRules;
        }

        public async Task<CreatedPrescriptionResponse> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
        {
            Prescription prescription = _mapper.Map<Prescription>(request);

            await _prescriptionRepository.AddAsync(prescription);

            CreatedPrescriptionResponse response = _mapper.Map<CreatedPrescriptionResponse>(prescription);
            return response;
        }
    }
}
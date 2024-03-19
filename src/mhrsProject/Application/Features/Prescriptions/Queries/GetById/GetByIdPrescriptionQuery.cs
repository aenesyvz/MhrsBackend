using Application.Features.Prescriptions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Prescriptions.Constants.PrescriptionsOperationClaims;

namespace Application.Features.Prescriptions.Queries.GetById;


public class GetByIdPrescriptionQuery : IRequest<GetByIdPrescriptionResponse>
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdPrescriptionQueryHandler : IRequestHandler<GetByIdPrescriptionQuery, GetByIdPrescriptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly PrescriptionBusinessRules _prescriptionBusinessRules;

        public GetByIdPrescriptionQueryHandler(IMapper mapper, IPrescriptionRepository prescriptionRepository, PrescriptionBusinessRules prescriptionBusinessRules)
        {
            _mapper = mapper;
            _prescriptionRepository = prescriptionRepository;
            _prescriptionBusinessRules = prescriptionBusinessRules;
        }

        public async Task<GetByIdPrescriptionResponse> Handle(GetByIdPrescriptionQuery request, CancellationToken cancellationToken)
        {
            Prescription? prescription = await _prescriptionRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _prescriptionBusinessRules.PrescriptionShouldExistWhenSelected(prescription);

            GetByIdPrescriptionResponse response = _mapper.Map<GetByIdPrescriptionResponse>(prescription);
            return response;
        }
    }
}
using Application.Features.Medicines.Contants;
using Application.Features.Medicines.Rules;
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
using static Application.Features.Medicines.Contants.MedicinesOperationClaims;

namespace Application.Features.Medicines.Commands.Create;


public class CreateMedicineCommand : IRequest<CreatedMedicineResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid MedicineCompanyId { get; set; }
    public string Name { get; set; }
    public string PurposeOfUsage { get; set; }
    public string SideEffects { get; set; }
    public string ConditionsToBeConsidired { get; set; }
    public string TermsOfUse { get; set; }
    public string ImageUrl { get; set; }

    public string[] Roles => new[] { Admin, Write, MedicinesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetMedicines" };

    public class CreateMedicineCommandHandler : IRequestHandler<CreateMedicineCommand, CreatedMedicineResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMedicineRepository _medicineRepository;
        private readonly MedicineBusinessRules _medicineBusinessRules;

        public CreateMedicineCommandHandler(IMapper mapper, IMedicineRepository medicineRepository,
                                         MedicineBusinessRules medicineBusinessRules)
        {
            _mapper = mapper;
            _medicineRepository = medicineRepository;
            _medicineBusinessRules = medicineBusinessRules;
        }

        public async Task<CreatedMedicineResponse> Handle(CreateMedicineCommand request, CancellationToken cancellationToken)
        {
            Medicine medicine = _mapper.Map<Medicine>(request);

            await _medicineRepository.AddAsync(medicine);

            CreatedMedicineResponse response = _mapper.Map<CreatedMedicineResponse>(medicine);
            return response;
        }
    }
}
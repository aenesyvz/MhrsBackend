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

namespace Application.Features.Medicines.Commands.Update;



public class UpdateMedicineCommand : IRequest<UpdatedMedicineResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid MedicineCompanyId { get; set; }
    public string Name { get; set; }
    public string PurposeOfUsage { get; set; }
    public string SideEffects { get; set; }
    public string ConditionsToBeConsidired { get; set; }
    public string TermsOfUse { get; set; }

    public string[] Roles => new[] { Admin, Write, MedicinesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetMedicines" };

    public class UpdateMedicineCommandHandler : IRequestHandler<UpdateMedicineCommand, UpdatedMedicineResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMedicineRepository _medicineRepository;
        private readonly MedicineBusinessRules _medicineBusinessRules;

        public UpdateMedicineCommandHandler(IMapper mapper, IMedicineRepository medicineRepository,
                                         MedicineBusinessRules medicineBusinessRules)
        {
            _mapper = mapper;
            _medicineRepository = medicineRepository;
            _medicineBusinessRules = medicineBusinessRules;
        }

        public async Task<UpdatedMedicineResponse> Handle(UpdateMedicineCommand request, CancellationToken cancellationToken)
        {
            Medicine? medicine = await _medicineRepository.GetAsync(predicate: m => m.Id == request.Id, cancellationToken: cancellationToken);
            await _medicineBusinessRules.MedicineShouldExistWhenSelected(medicine);
            medicine = _mapper.Map(request, medicine);

            await _medicineRepository.UpdateAsync(medicine!);

            UpdatedMedicineResponse response = _mapper.Map<UpdatedMedicineResponse>(medicine);
            return response;
        }
    }
}
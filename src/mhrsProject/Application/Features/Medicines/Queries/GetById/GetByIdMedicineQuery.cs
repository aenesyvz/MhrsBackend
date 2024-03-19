using Application.Features.Medicines.Rules;
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
using static Application.Features.Medicines.Contants.MedicinesOperationClaims;

namespace Application.Features.Medicines.Queries.GetById;

public class GetByIdMedicineQuery : IRequest<GetByIdMedicineResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdMedicineQueryHandler : IRequestHandler<GetByIdMedicineQuery, GetByIdMedicineResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMedicineRepository _medicineRepository;
        private readonly MedicineBusinessRules _medicineBusinessRules;

        public GetByIdMedicineQueryHandler(IMapper mapper, IMedicineRepository medicineRepository, MedicineBusinessRules medicineBusinessRules)
        {
            _mapper = mapper;
            _medicineRepository = medicineRepository;
            _medicineBusinessRules = medicineBusinessRules;
        }

        public async Task<GetByIdMedicineResponse> Handle(GetByIdMedicineQuery request, CancellationToken cancellationToken)
        {
            Medicine? medicine = await _medicineRepository.GetAsync(predicate: m => m.Id == request.Id, cancellationToken: cancellationToken);
            await _medicineBusinessRules.MedicineShouldExistWhenSelected(medicine);

            GetByIdMedicineResponse response = _mapper.Map<GetByIdMedicineResponse>(medicine);
            return response;
        }
    }
}

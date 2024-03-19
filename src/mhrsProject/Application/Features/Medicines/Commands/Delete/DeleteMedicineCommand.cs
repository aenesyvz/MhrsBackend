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

namespace Application.Features.Medicines.Commands.Delete;


public class DeleteMedicineCommand : IRequest<DeletedMedicineResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, MedicinesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetMedicines" };

    public class DeleteMedicineCommandHandler : IRequestHandler<DeleteMedicineCommand, DeletedMedicineResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMedicineRepository _medicineRepository;
        private readonly MedicineBusinessRules _medicineBusinessRules;

        public DeleteMedicineCommandHandler(IMapper mapper, IMedicineRepository medicineRepository,
                                         MedicineBusinessRules medicineBusinessRules)
        {
            _mapper = mapper;
            _medicineRepository = medicineRepository;
            _medicineBusinessRules = medicineBusinessRules;
        }

        public async Task<DeletedMedicineResponse> Handle(DeleteMedicineCommand request, CancellationToken cancellationToken)
        {
            Medicine? medicine = await _medicineRepository.GetAsync(predicate: m => m.Id == request.Id, cancellationToken: cancellationToken);
            await _medicineBusinessRules.MedicineShouldExistWhenSelected(medicine);

            await _medicineRepository.DeleteAsync(medicine!);

            DeletedMedicineResponse response = _mapper.Map<DeletedMedicineResponse>(medicine);
            return response;
        }
    }
}

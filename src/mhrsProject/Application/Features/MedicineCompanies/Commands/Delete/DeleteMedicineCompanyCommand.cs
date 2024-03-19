using Application.Features.MedicineCompanies.Constants;
using Application.Features.MedicineCompanies.Rules;
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
using static Application.Features.MedicineCompanies.Constants.MedicineCompaniesOperationClaims;

namespace Application.Features.MedicineCompanies.Commands.Delete;

public class DeleteMedicineCompanyCommand : IRequest<DeletedMedicineCompanyResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, MedicineCompaniesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetMedicineCompanies" };

    public class DeleteMedicineCompanyCommandHandler : IRequestHandler<DeleteMedicineCompanyCommand, DeletedMedicineCompanyResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMedicineCompanyRepository _medicineCompanyRepository;
        private readonly MedicineCompanyBusinessRules _medicineCompanyBusinessRules;

        public DeleteMedicineCompanyCommandHandler(IMapper mapper, IMedicineCompanyRepository medicineCompanyRepository,
                                         MedicineCompanyBusinessRules medicineCompanyBusinessRules)
        {
            _mapper = mapper;
            _medicineCompanyRepository = medicineCompanyRepository;
            _medicineCompanyBusinessRules = medicineCompanyBusinessRules;
        }

        public async Task<DeletedMedicineCompanyResponse> Handle(DeleteMedicineCompanyCommand request, CancellationToken cancellationToken)
        {
            MedicineCompany? medicineCompany = await _medicineCompanyRepository.GetAsync(predicate: mc => mc.Id == request.Id, cancellationToken: cancellationToken);
            await _medicineCompanyBusinessRules.MedicineCompanyShouldExistWhenSelected(medicineCompany);

            await _medicineCompanyRepository.DeleteAsync(medicineCompany!);

            DeletedMedicineCompanyResponse response = _mapper.Map<DeletedMedicineCompanyResponse>(medicineCompany);
            return response;
        }
    }
}
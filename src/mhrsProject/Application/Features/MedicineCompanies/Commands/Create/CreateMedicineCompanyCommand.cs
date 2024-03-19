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

namespace Application.Features.MedicineCompanies.Commands.Create;

public class CreateMedicineCompanyCommand : IRequest<CreatedMedicineCompanyResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string TaxOffice { get; set; }
    public string TaxNumber { get; set; }

    public string[] Roles => new[] { Admin, Write, MedicineCompaniesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetMedicineCompanies" };

    public class CreateMedicineCompanyCommandHandler : IRequestHandler<CreateMedicineCompanyCommand, CreatedMedicineCompanyResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMedicineCompanyRepository _medicineCompanyRepository;
        private readonly MedicineCompanyBusinessRules _medicineCompanyBusinessRules;

        public CreateMedicineCompanyCommandHandler(IMapper mapper, IMedicineCompanyRepository medicineCompanyRepository,
                                         MedicineCompanyBusinessRules medicineCompanyBusinessRules)
        {
            _mapper = mapper;
            _medicineCompanyRepository = medicineCompanyRepository;
            _medicineCompanyBusinessRules = medicineCompanyBusinessRules;
        }

        public async Task<CreatedMedicineCompanyResponse> Handle(CreateMedicineCompanyCommand request, CancellationToken cancellationToken)
        {
            MedicineCompany medicineCompany = _mapper.Map<MedicineCompany>(request);

            await _medicineCompanyRepository.AddAsync(medicineCompany);

            CreatedMedicineCompanyResponse response = _mapper.Map<CreatedMedicineCompanyResponse>(medicineCompany);
            return response;
        }
    }
}
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

namespace Application.Features.MedicineCompanies.Commands.Update;

public class UpdateMedicineCompanyCommand : IRequest<UpdatedMedicineCompanyResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string TaxOffice { get; set; }
    public string TaxNumber { get; set; }
    public string ImageUrl { get; set; }

    public string[] Roles => new[] { Admin, Write, MedicineCompaniesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetMedicineCompanies" };

    public class UpdateMedicineCompanyCommandHandler : IRequestHandler<UpdateMedicineCompanyCommand, UpdatedMedicineCompanyResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMedicineCompanyRepository _medicineCompanyRepository;
        private readonly MedicineCompanyBusinessRules _medicineCompanyBusinessRules;

        public UpdateMedicineCompanyCommandHandler(IMapper mapper, IMedicineCompanyRepository medicineCompanyRepository,
                                         MedicineCompanyBusinessRules medicineCompanyBusinessRules)
        {
            _mapper = mapper;
            _medicineCompanyRepository = medicineCompanyRepository;
            _medicineCompanyBusinessRules = medicineCompanyBusinessRules;
        }

        public async Task<UpdatedMedicineCompanyResponse> Handle(UpdateMedicineCompanyCommand request, CancellationToken cancellationToken)
        {
            MedicineCompany? medicineCompany = await _medicineCompanyRepository.GetAsync(predicate: mc => mc.Id == request.Id, cancellationToken: cancellationToken);
            await _medicineCompanyBusinessRules.MedicineCompanyShouldExistWhenSelected(medicineCompany);
            medicineCompany = _mapper.Map(request, medicineCompany);

            await _medicineCompanyRepository.UpdateAsync(medicineCompany!);

            UpdatedMedicineCompanyResponse response = _mapper.Map<UpdatedMedicineCompanyResponse>(medicineCompany);
            return response;
        }
    }
}

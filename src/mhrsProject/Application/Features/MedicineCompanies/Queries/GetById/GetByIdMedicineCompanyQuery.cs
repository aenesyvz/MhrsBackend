using Application.Features.MedicineCompanies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.MedicineCompanies.Constants.MedicineCompaniesOperationClaims;

namespace Application.Features.MedicineCompanies.Queries.GetById;
public class GetByIdMedicineCompanyQuery : IRequest<GetByIdMedicineCompanyResponse>
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdMedicineCompanyQueryHandler : IRequestHandler<GetByIdMedicineCompanyQuery, GetByIdMedicineCompanyResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMedicineCompanyRepository _medicineCompanyRepository;
        private readonly MedicineCompanyBusinessRules _medicineCompanyBusinessRules;

        public GetByIdMedicineCompanyQueryHandler(IMapper mapper, IMedicineCompanyRepository medicineCompanyRepository, MedicineCompanyBusinessRules medicineCompanyBusinessRules)
        {
            _mapper = mapper;
            _medicineCompanyRepository = medicineCompanyRepository;
            _medicineCompanyBusinessRules = medicineCompanyBusinessRules;
        }

        public async Task<GetByIdMedicineCompanyResponse> Handle(GetByIdMedicineCompanyQuery request, CancellationToken cancellationToken)
        {
            MedicineCompany? medicineCompany = await _medicineCompanyRepository.GetAsync(predicate: mc => mc.Id == request.Id, cancellationToken: cancellationToken);
            await _medicineCompanyBusinessRules.MedicineCompanyShouldExistWhenSelected(medicineCompany);

            GetByIdMedicineCompanyResponse response = _mapper.Map<GetByIdMedicineCompanyResponse>(medicineCompany);
            return response;
        }
    }
}

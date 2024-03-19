using Application.Features.MedicineCompanies.Rules;
using Application.Features.Medicines.Queries.GetById;
using Application.Services.Medicines;
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

namespace Application.Features.MedicineCompanies.Queries.GetByIdWithMedicines;


public class GetByIdMedicineCompanyWithMedicinesQuery : IRequest<GetByIdMedicineCompanyWithMedicinesResponse>
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdMedicineCompanyWithMedicinesQueryHandler : IRequestHandler<GetByIdMedicineCompanyWithMedicinesQuery, GetByIdMedicineCompanyWithMedicinesResponse>
    {
        private readonly IMedicineCompanyRepository _medicineCompanyRepository;
        private readonly IMedicineRepository _medicineRepository;
        private readonly IMapper _mapper;
        private readonly MedicineCompanyBusinessRules _medicineCompanyBusinessRules;
        private readonly IMedicinesService _medicinesService;
        public GetByIdMedicineCompanyWithMedicinesQueryHandler(IMedicineCompanyRepository medicineCompanyRepository, IMapper mapper, MedicineCompanyBusinessRules medicineCompanyBusinessRules, IMedicinesService medicinesService, IMedicineRepository medicineRepository)
        {
            _medicineCompanyRepository = medicineCompanyRepository;
            _mapper = mapper;
            _medicineCompanyBusinessRules = medicineCompanyBusinessRules;
            _medicinesService = medicinesService;
            _medicineRepository = medicineRepository;
        }
        public async Task<GetByIdMedicineCompanyWithMedicinesResponse> Handle(GetByIdMedicineCompanyWithMedicinesQuery request, CancellationToken cancellationToken)
        {
            MedicineCompany? medicineCompany = await _medicineCompanyRepository.GetAsync(
                      predicate: m => m.Id == request.Id
                );

            await _medicineCompanyBusinessRules.MedicineCompanyShouldExistWhenSelected(medicineCompany);

            var medicines = await _medicineRepository.GetListAsync(
                    predicate: x => x.MedicineCompanyId == medicineCompany!.Id,
                    orderBy: c => c.OrderBy(c => c.Name),  
                    cancellationToken:cancellationToken
               );

            IList<GetByIdMedicineResponse> getByIdMedicineResponse = _mapper.Map<IList<GetByIdMedicineResponse>>(medicines);

            GetByIdMedicineCompanyWithMedicinesResponse response = _mapper.Map<GetByIdMedicineCompanyWithMedicinesResponse>(medicineCompany);

            response.Medicines = getByIdMedicineResponse;
            return response;
        }
    }

}


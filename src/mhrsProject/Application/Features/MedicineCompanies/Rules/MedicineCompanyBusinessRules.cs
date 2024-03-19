using Application.Features.MedicineCompanies.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MedicineCompanies.Rules;
public class MedicineCompanyBusinessRules : BaseBusinessRules
{
    private readonly IMedicineCompanyRepository _medicineCompanyRepository;

    public MedicineCompanyBusinessRules(IMedicineCompanyRepository medicineCompanyRepository)
    {
        _medicineCompanyRepository = medicineCompanyRepository;
    }

    public Task MedicineCompanyShouldExistWhenSelected(MedicineCompany? medicineCompany)
    {
        if (medicineCompany == null)
            throw new BusinessException(MedicineCompaniesBusinessMessages.MedicineCompanyNotExists);
        return Task.CompletedTask;
    }

    public async Task MedicineCompanyIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        MedicineCompany? medicineCompany = await _medicineCompanyRepository.GetAsync(
            predicate: mc => mc.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await MedicineCompanyShouldExistWhenSelected(medicineCompany);
    }
}

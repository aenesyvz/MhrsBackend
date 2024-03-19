using Application.Features.Medicines.Queries.GetById;
using Core.Application.Dtos;

namespace Application.Features.MedicineCompanies.Queries.GetByIdWithMedicines;

public class GetByIdMedicineCompanyWithMedicinesResponse : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string TaxOffice { get; set; }
    public string TaxNumber { get; set; }
    public string ImageUrl { get; set; }
    public IList<GetByIdMedicineResponse> Medicines { get; set; }
}


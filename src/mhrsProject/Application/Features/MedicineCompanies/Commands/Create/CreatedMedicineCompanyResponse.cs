using Core.Application.Responses;

namespace Application.Features.MedicineCompanies.Commands.Create;

public class CreatedMedicineCompanyResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string TaxOffice { get; set; }
    public string TaxNumber { get; set; }
    public string? ImageUrl { get; set; }
}

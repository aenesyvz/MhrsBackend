using Core.Application.Responses;

namespace Application.Features.MedicineCompanies.Commands.Delete;

public class DeletedMedicineCompanyResponse : IResponse
{
    public Guid Id { get; set; }
}

using Core.Application.Responses;

namespace Application.Features.Medicines.Commands.Delete;

public class DeletedMedicineResponse : IResponse
{
    public Guid Id { get; set; }
}

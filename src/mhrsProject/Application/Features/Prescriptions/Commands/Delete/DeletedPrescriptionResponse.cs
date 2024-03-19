using Core.Application.Responses;

namespace Application.Features.Prescriptions.Commands.Delete;

public class DeletedPrescriptionResponse : IResponse
{
    public Guid Id { get; set; }
}

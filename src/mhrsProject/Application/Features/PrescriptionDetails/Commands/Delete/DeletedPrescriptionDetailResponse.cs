using Core.Application.Responses;

namespace Application.Features.PrescriptionDetails.Commands.Delete;

public class DeletedPrescriptionDetailResponse : IResponse
{
    public Guid Id { get; set; }
}

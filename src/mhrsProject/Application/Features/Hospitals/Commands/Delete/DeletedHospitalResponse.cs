using Core.Application.Responses;

namespace Application.Features.Hospitals.Commands.Delete;

public class DeletedHospitalResponse : IResponse
{
    public Guid Id { get; set; }
}

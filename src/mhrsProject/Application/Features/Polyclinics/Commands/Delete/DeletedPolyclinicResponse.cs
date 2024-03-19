using Core.Application.Responses;

namespace Application.Features.Polyclinics.Commands.Delete;

public class DeletedPolyclinicResponse : IResponse
{
    public Guid Id { get; set; }
}

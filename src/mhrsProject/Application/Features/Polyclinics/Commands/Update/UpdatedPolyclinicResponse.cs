using Core.Application.Responses;

namespace Application.Features.Polyclinics.Commands.Update;

public class UpdatedPolyclinicResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

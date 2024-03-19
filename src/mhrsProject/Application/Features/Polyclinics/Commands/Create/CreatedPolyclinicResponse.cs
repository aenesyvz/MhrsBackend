using Core.Application.Responses;

namespace Application.Features.Polyclinics.Commands.Create;

public class CreatedPolyclinicResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

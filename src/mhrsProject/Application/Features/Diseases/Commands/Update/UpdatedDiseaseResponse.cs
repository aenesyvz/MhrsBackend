using Core.Application.Responses;

namespace Application.Features.Diseases.Commands.Update;

public class UpdatedDiseaseResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid PolyclinicId { get; set; }
    public string Name { get; set; }
}

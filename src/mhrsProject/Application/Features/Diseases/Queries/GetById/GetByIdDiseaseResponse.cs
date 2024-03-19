using Core.Application.Responses;

namespace Application.Features.Diseases.Queries.GetById;

public class GetByIdDiseaseResponse : IResponse
{
    public Guid Id { get; set; }
    public string PolyclinicName { get; set; }
    public string Name { get; set; }
}
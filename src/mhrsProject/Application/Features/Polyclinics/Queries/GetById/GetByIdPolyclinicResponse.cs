using Core.Application.Responses;

namespace Application.Features.Polyclinics.Queries.GetById;

public class GetByIdPolyclinicResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
using Core.Application.Responses;

namespace Application.Features.Diseases.Commands.Delete;

public class DeletedDiseaseResponse : IResponse
{
    public Guid Id { get; set; }
}

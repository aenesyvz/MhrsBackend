using Core.Application.Dtos;

namespace Application.Features.Diseases.Queries.GetListByDynamic;

public class GetListDiseaseByDynamicModelListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid PolyclinicId { get; set; }
    public string Name { get; set; }
}

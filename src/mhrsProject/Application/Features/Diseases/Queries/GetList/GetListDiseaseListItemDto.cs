using Core.Application.Dtos;

namespace Application.Features.Diseases.Queries.GetList;

public class GetListDiseaseListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid PolyclinicId { get; set; }
    public string PolyclinicName { get; set; }
    public string Name { get; set; }
}

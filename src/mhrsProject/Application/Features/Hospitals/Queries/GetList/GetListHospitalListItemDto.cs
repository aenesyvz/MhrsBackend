using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.Hospitals.Queries.GetList;

public class GetListHospitalListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public HospitalClassType HospitalClassType { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string CityName { get; set; }
    public string DistrictName { get; set; }
}

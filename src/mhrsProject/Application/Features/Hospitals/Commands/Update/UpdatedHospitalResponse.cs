using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.Hospitals.Commands.Update;

public class UpdatedHospitalResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public HospitalClassType HospitalClassType { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public Guid CityId { get; set; }
    public Guid DistrictId { get; set; }
}

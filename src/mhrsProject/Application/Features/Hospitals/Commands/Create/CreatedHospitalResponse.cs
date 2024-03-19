using Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hospitals.Commands.Create;
public class CreatedHospitalResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string HospitalClassType { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public Guid CityId { get; set; }
    public Guid DistrictId { get; set; }
}

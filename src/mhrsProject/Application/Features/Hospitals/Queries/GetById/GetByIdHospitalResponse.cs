using Core.Application.Responses;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hospitals.Queries.GetById;

public class GetByIdHospitalResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public HospitalClassType HospitalClassType { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string CityName { get; set; }
    public string DistrictName { get; set; }
}
using Application.Features.Hospitals.Commands.Create;
using Application.Features.Hospitals.Commands.Delete;
using Application.Features.Hospitals.Commands.Update;
using Application.Features.Hospitals.Queries.GetById;
using Application.Features.Hospitals.Queries.GetList;
using Application.Features.Hospitals.Queries.GetListByDynamic;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hospitals.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Hospital, CreateHospitalCommand>().ReverseMap();
        CreateMap<Hospital, CreatedHospitalResponse>().ReverseMap();

        CreateMap<Hospital, UpdateHospitalCommand>().ReverseMap();
        CreateMap<Hospital, UpdatedHospitalResponse>().ReverseMap();

        CreateMap<Hospital, DeleteHospitalCommand>().ReverseMap();
        CreateMap<Hospital, DeletedHospitalResponse>().ReverseMap();

        CreateMap<Hospital, GetByIdHospitalResponse>()
            .ForMember(destinationMember: h => h.CityName, memberOptions: opt => opt.MapFrom(c => c.City.Name))
            .ForMember(destinationMember: h => h.DistrictName, memberOptions: opt => opt.MapFrom(c => c.District.Name))
            .ReverseMap();

        CreateMap<Hospital, GetListHospitalListItemDto>()
            .ForMember(destinationMember: h => h.CityName, memberOptions: opt => opt.MapFrom(c => c.City.Name))
            .ForMember(destinationMember: h => h.DistrictName, memberOptions: opt => opt.MapFrom(c => c.District.Name))
            .ReverseMap();

        CreateMap<Hospital, GetListHospitalByDynamicModelListItemDto>()
            .ForMember(destinationMember: h => h.CityName, memberOptions: opt => opt.MapFrom(c => c.City.Name))
            .ForMember(destinationMember: h => h.DistrictName, memberOptions: opt => opt.MapFrom(c => c.District.Name))
            .ReverseMap();


        CreateMap<IPaginate<Hospital>, GetListResponse<GetListHospitalListItemDto>>().ReverseMap();
    }
}
using Application.Features.Districts.Commands.Create;
using Application.Features.Districts.Commands.Delete;
using Application.Features.Districts.Commands.Update;
using Application.Features.Districts.Queries.GetById;
using Application.Features.Districts.Queries.GetList;
using Application.Features.Districts.Queries.GetListByCityId;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Districts.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<District, CreateDistrictCommand>().ReverseMap();
        CreateMap<District, CreatedDistrictResponse>().ReverseMap();

        CreateMap<District, UpdateDistrictCommand>().ReverseMap();
        CreateMap<District, UpdatedDistrictResponse>().ReverseMap();

        CreateMap<District, DeleteDistrictCommand>().ReverseMap();
        CreateMap<District, DeletedDistrictResponse>().ReverseMap();

        CreateMap<District, GetByIdDistrictResponse>()
            .ForMember(destinationMember: d => d.CityName, memberOptions: opt => opt.MapFrom(c => c.City.Name))
            .ReverseMap();
        CreateMap<District, GetListDistrictListItemDto>()
            .ForMember(destinationMember: d => d.CityName, memberOptions: opt => opt.MapFrom(c => c.City.Name))
            .ReverseMap();
        CreateMap<District, GetListDistrictByCityIdModelListItemDto>()
            .ForMember(destinationMember: d => d.CityName, memberOptions: opt => opt.MapFrom(c => c.City.Name))
            .ReverseMap();

        CreateMap<IPaginate<District>, GetListResponse<GetListDistrictListItemDto>>().ReverseMap();
    }
}
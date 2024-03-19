using Application.Features.Diseases.Commands.Create;
using Application.Features.Diseases.Commands.Delete;
using Application.Features.Diseases.Commands.Update;
using Application.Features.Diseases.Queries.GetById;
using Application.Features.Diseases.Queries.GetList;
using Application.Features.Diseases.Queries.GetListByDynamic;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Diseases.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Disease, CreateDiseaseCommand>().ReverseMap();
        CreateMap<Disease, CreatedDiseaseResponse>().ReverseMap();

        CreateMap<Disease, UpdateDiseaseCommand>().ReverseMap();
        CreateMap<Disease, UpdatedDiseaseResponse>().ReverseMap();

        CreateMap<Disease, DeleteDiseaseCommand>().ReverseMap();
        CreateMap<Disease, DeletedDiseaseResponse>().ReverseMap();

        CreateMap<Disease, GetByIdDiseaseResponse>()
            .ForMember(destinationMember: d => d.PolyclinicName, memberOptions: opt => opt.MapFrom(p => p.Polyclinic.Name))
            .ReverseMap();
        CreateMap<Disease, GetListDiseaseByDynamicModelListItemDto>().ReverseMap();
        CreateMap<Disease, GetListDiseaseListItemDto>()
            .ForMember(destinationMember: d => d.PolyclinicName, memberOptions: opt => opt.MapFrom(p => p.Polyclinic.Name))
            .ReverseMap();


        CreateMap<IPaginate<Disease>, GetListResponse<GetListDiseaseListItemDto>>().ReverseMap();
    }
}
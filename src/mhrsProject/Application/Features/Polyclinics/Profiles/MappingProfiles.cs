using Application.Features.Polyclinics.Commands.Create;
using Application.Features.Polyclinics.Commands.Delete;
using Application.Features.Polyclinics.Commands.Update;
using Application.Features.Polyclinics.Queries.GetById;
using Application.Features.Polyclinics.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Polyclinics.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Polyclinic, CreatePolyclinicCommand>().ReverseMap();
        CreateMap<Polyclinic, CreatedPolyclinicResponse>().ReverseMap();
        CreateMap<Polyclinic, UpdatePolyclinicCommand>().ReverseMap();
        CreateMap<Polyclinic, UpdatedPolyclinicResponse>().ReverseMap();
        CreateMap<Polyclinic, DeletePolyclinicCommand>().ReverseMap();
        CreateMap<Polyclinic, DeletedPolyclinicResponse>().ReverseMap();
        CreateMap<Polyclinic, GetByIdPolyclinicResponse>().ReverseMap();
        CreateMap<Polyclinic, GetListPolyclinicListItemDto>().ReverseMap();
        CreateMap<IPaginate<Polyclinic>, GetListResponse<GetListPolyclinicListItemDto>>().ReverseMap();
    }
}
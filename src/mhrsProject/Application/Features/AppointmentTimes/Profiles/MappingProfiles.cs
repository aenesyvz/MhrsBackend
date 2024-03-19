using Application.Features.AppointmentTimes.Commands.Create;
using Application.Features.AppointmentTimes.Commands.Delete;
using Application.Features.AppointmentTimes.Commands.Update;
using Application.Features.AppointmentTimes.Queries.GetById;
using Application.Features.AppointmentTimes.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AppointmentTimes.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AppointmentTime, CreateAppointmentTimeCommand>().ReverseMap();
        CreateMap<AppointmentTime, CreatedAppointmentTimeResponse>().ReverseMap();
        CreateMap<AppointmentTime, UpdateAppointmentTimeCommand>().ReverseMap();
        CreateMap<AppointmentTime, UpdatedAppointmentTimeResponse>().ReverseMap();
        CreateMap<AppointmentTime, DeleteAppointmentTimeCommand>().ReverseMap();
        CreateMap<AppointmentTime, DeletedAppointmentTimeResponse>().ReverseMap();
        CreateMap<AppointmentTime, GetByIdAppointmentTimeResponse>().ReverseMap();
        CreateMap<AppointmentTime, GetListAppointmentTimeListItemDto>().ReverseMap();
        CreateMap<IPaginate<AppointmentTime>, GetListResponse<GetListAppointmentTimeListItemDto>>().ReverseMap();
    }
}

using Application.Features.Appointments.Commands.Create;
using Application.Features.Appointments.Commands.Delete;
using Application.Features.Appointments.Commands.Update;
using Application.Features.Appointments.Queries.GetById;
using Application.Features.Appointments.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Appointments.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Appointment, CreateAppointmentCommand>().ReverseMap();
        CreateMap<Appointment, CreatedAppointmentResponse>().ReverseMap();
        CreateMap<Appointment, UpdateAppointmentCommand>().ReverseMap();
        CreateMap<Appointment, UpdatedAppointmentResponse>().ReverseMap();
        CreateMap<Appointment, DeleteAppointmentCommand>().ReverseMap();
        CreateMap<Appointment, DeletedAppointmentResponse>().ReverseMap();
        CreateMap<Appointment, GetByIdAppointmentResponse>().ReverseMap();
        CreateMap<Appointment, GetListAppointmentListItemDto>().ReverseMap();

        //CreateMap<Appointment, GetListByDynamicForPatientInfoModelItem>()
        //    .ForMember(destinationMember: a => a.HospitalName, memberOptions: opt => opt.MapFrom(h => h.Hospital.Name))
        //    .ForMember(destinationMember: a => a.PolyclinicName, memberOptions: opt => opt.MapFrom(p => p.Polyclinic.Name))
        //    .ReverseMap();
        //CreateMap<IList<Appointment>, GetListByDynamicForPatientInfoModelItem>().ReverseMap();
        CreateMap<IPaginate<Appointment>, GetListResponse<GetListAppointmentListItemDto>>().ReverseMap();
    }
}
using Application.Features.Prescriptions.Commands.Create;
using Application.Features.Prescriptions.Commands.Delete;
using Application.Features.Prescriptions.Commands.Update;
using Application.Features.Prescriptions.Queries.GetById;
using Application.Features.Prescriptions.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Prescriptions.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Prescription, CreatePrescriptionCommand>().ReverseMap();
        CreateMap<Prescription, CreatedPrescriptionResponse>().ReverseMap();
        CreateMap<Prescription, UpdatePrescriptionCommand>().ReverseMap();
        CreateMap<Prescription, UpdatedPrescriptionResponse>().ReverseMap();
        CreateMap<Prescription, DeletePrescriptionCommand>().ReverseMap();
        CreateMap<Prescription, DeletedPrescriptionResponse>().ReverseMap();
        CreateMap<Prescription, GetByIdPrescriptionResponse>().ReverseMap();
        CreateMap<Prescription, GetListPrescriptionListItemDto>().ReverseMap();
        CreateMap<IPaginate<Prescription>, GetListResponse<GetListPrescriptionListItemDto>>().ReverseMap();
    }
}
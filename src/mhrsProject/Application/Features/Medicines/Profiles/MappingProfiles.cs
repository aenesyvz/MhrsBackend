using Application.Features.Medicines.Commands.Create;
using Application.Features.Medicines.Commands.Delete;
using Application.Features.Medicines.Commands.Update;
using Application.Features.Medicines.Queries.GetById;
using Application.Features.Medicines.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Medicines.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Medicine, CreateMedicineCommand>().ReverseMap();
        CreateMap<Medicine, CreatedMedicineResponse>().ReverseMap();
        CreateMap<Medicine, UpdateMedicineCommand>().ReverseMap();
        CreateMap<Medicine, UpdatedMedicineResponse>().ReverseMap();
        CreateMap<Medicine, DeleteMedicineCommand>().ReverseMap();
        CreateMap<Medicine, DeletedMedicineResponse>().ReverseMap();
        CreateMap<Medicine, GetByIdMedicineResponse>().ReverseMap();
        CreateMap<Medicine, GetListMedicineListItemDto>().ReverseMap();
        CreateMap<IPaginate<Medicine>, GetListResponse<GetListMedicineListItemDto>>().ReverseMap();
    }
}
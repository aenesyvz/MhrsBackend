using Application.Features.MedicineCompanies.Commands.Create;
using Application.Features.MedicineCompanies.Commands.Delete;
using Application.Features.MedicineCompanies.Commands.Update;
using Application.Features.MedicineCompanies.Queries.GetById;
using Application.Features.MedicineCompanies.Queries.GetByIdWithMedicines;
using Application.Features.MedicineCompanies.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MedicineCompanies.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<MedicineCompany, CreateMedicineCompanyCommand>().ReverseMap();
        CreateMap<MedicineCompany, CreatedMedicineCompanyResponse>().ReverseMap();

        CreateMap<MedicineCompany, UpdateMedicineCompanyCommand>().ReverseMap();
        CreateMap<MedicineCompany, UpdatedMedicineCompanyResponse>().ReverseMap();

        CreateMap<MedicineCompany, DeleteMedicineCompanyCommand>().ReverseMap();
        CreateMap<MedicineCompany, DeletedMedicineCompanyResponse>().ReverseMap();

        CreateMap<MedicineCompany, GetByIdMedicineCompanyResponse>().ReverseMap();
        CreateMap<MedicineCompany, GetListMedicineCompanyListItemDto>().ReverseMap();
        CreateMap<MedicineCompany, GetByIdMedicineCompanyWithMedicinesResponse>()
            .ReverseMap();

        CreateMap<IPaginate<MedicineCompany>, GetListResponse<GetListMedicineCompanyListItemDto>>().ReverseMap();
    }
}

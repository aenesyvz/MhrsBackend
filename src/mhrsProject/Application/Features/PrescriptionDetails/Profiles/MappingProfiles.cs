using Application.Features.PrescriptionDetails.Commands.Create;
using Application.Features.PrescriptionDetails.Commands.Delete;
using Application.Features.PrescriptionDetails.Commands.Update;
using Application.Features.PrescriptionDetails.Queries.GetById;
using Application.Features.PrescriptionDetails.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.PrescriptionDetails.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<PrescriptionDetail, CreatePrescriptionDetailCommand>().ReverseMap();
        CreateMap<PrescriptionDetail, CreatedPrescriptionDetailResponse>().ReverseMap();
        CreateMap<PrescriptionDetail, UpdatePrescriptionDetailCommand>().ReverseMap();
        CreateMap<PrescriptionDetail, UpdatedPrescriptionDetailResponse>().ReverseMap();
        CreateMap<PrescriptionDetail, DeletePrescriptionDetailCommand>().ReverseMap();
        CreateMap<PrescriptionDetail, DeletedPrescriptionDetailResponse>().ReverseMap();
        CreateMap<PrescriptionDetail, GetByIdPrescriptionDetailResponse>().ReverseMap();
        CreateMap<PrescriptionDetail, GetListPrescriptionDetailListItemDto>().ReverseMap();
        CreateMap<IPaginate<PrescriptionDetail>, GetListResponse<GetListPrescriptionDetailListItemDto>>().ReverseMap();
    }
}
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Application.Features.MedicineCompanies.Constants.MedicineCompaniesOperationClaims;

namespace Application.Features.MedicineCompanies.Queries.GetList;

public class GetListMedicineCompanyQuery : IRequest<GetListResponse<GetListMedicineCompanyListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListMedicineCompanies({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetMedicineCompanies";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListMedicineCompanyQueryHandler : IRequestHandler<GetListMedicineCompanyQuery, GetListResponse<GetListMedicineCompanyListItemDto>>
    {
        private readonly IMedicineCompanyRepository _medicineCompanyRepository;
        private readonly IMapper _mapper;

        public GetListMedicineCompanyQueryHandler(IMedicineCompanyRepository medicineCompanyRepository, IMapper mapper)
        {
            _medicineCompanyRepository = medicineCompanyRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListMedicineCompanyListItemDto>> Handle(GetListMedicineCompanyQuery request, CancellationToken cancellationToken)
        {
            IPaginate<MedicineCompany> medicineCompanies = await _medicineCompanyRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListMedicineCompanyListItemDto> response = _mapper.Map<GetListResponse<GetListMedicineCompanyListItemDto>>(medicineCompanies);
            return response;
        }
    }
}
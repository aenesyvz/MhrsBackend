using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Application.Features.Polyclinics.Contants.PolyclinicsOperationClaims;

namespace Application.Features.Polyclinics.Queries.GetList;

public class GetListPolyclinicQuery : IRequest<GetListResponse<GetListPolyclinicListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListPolyclinics({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetPolyclinics";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListPolyclinicQueryHandler : IRequestHandler<GetListPolyclinicQuery, GetListResponse<GetListPolyclinicListItemDto>>
    {
        private readonly IPolyclinicRepository _polyclinicRepository;
        private readonly IMapper _mapper;

        public GetListPolyclinicQueryHandler(IPolyclinicRepository polyclinicRepository, IMapper mapper)
        {
            _polyclinicRepository = polyclinicRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListPolyclinicListItemDto>> Handle(GetListPolyclinicQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Polyclinic> polyclinics = await _polyclinicRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListPolyclinicListItemDto> response = _mapper.Map<GetListResponse<GetListPolyclinicListItemDto>>(polyclinics);
            return response;
        }
    }
}
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Cities.Contants.CitiesOperationClaims;

namespace Application.Features.Cities.Queries.GetList;

public class GetListCityQuery : IRequest<IList<GetListCityListItemDto>>, ICachableRequest
{
    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListCities";
    public string CacheGroupKey => "GetCities";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListCityQueryHandler : IRequestHandler<GetListCityQuery, IList<GetListCityListItemDto>>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public GetListCityQueryHandler(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<IList<GetListCityListItemDto>> Handle(GetListCityQuery request, CancellationToken cancellationToken)
        {

            IList<City> cities = await _cityRepository.GetListWithoutAsync();

            IList<GetListCityListItemDto> response = _mapper.Map<IList<GetListCityListItemDto>>(cities);
            return response;
        }
    }
}
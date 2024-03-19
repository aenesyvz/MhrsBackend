using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Districts.Queries.GetListByCityId;

public class GetListDistrictByCityIdQuery : IRequest<IList<GetListDistrictByCityIdModelListItemDto>>
{
    public Guid CityId { get; set; }
    public class GetListDistrictByCityIdQueryHandler : IRequestHandler<GetListDistrictByCityIdQuery, IList<GetListDistrictByCityIdModelListItemDto>>
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly IMapper _mapper;

        public GetListDistrictByCityIdQueryHandler(IDistrictRepository districtRepository, IMapper mapper)
        {
            _districtRepository = districtRepository;
            _mapper = mapper;
        }
        public async Task<IList<GetListDistrictByCityIdModelListItemDto>> Handle(GetListDistrictByCityIdQuery request, CancellationToken cancellationToken)
        {
            IList<District> districts = await _districtRepository.GetListDistrictByCityIdAsync(request.CityId);

            var response = _mapper.Map<IList<GetListDistrictByCityIdModelListItemDto>>(districts);

            return response;


        }
    }
}

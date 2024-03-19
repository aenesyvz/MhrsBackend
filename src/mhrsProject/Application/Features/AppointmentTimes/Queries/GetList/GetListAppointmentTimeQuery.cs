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
using static Application.Features.AppointmentTimes.Contants.AppointmentTimesOperationClaims;

namespace Application.Features.AppointmentTimes.Queries.GetList;
public class GetListAppointmentTimeQuery : IRequest<IList<GetListAppointmentTimeListItemDto>>, ICachableRequest
{

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListAppointmentTimes";
    public string CacheGroupKey => "GetAppointmentTimes";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListAppointmentTimeQueryHandler : IRequestHandler<GetListAppointmentTimeQuery, IList<GetListAppointmentTimeListItemDto>>
    {
        private readonly IAppointmentTimeRepository _appointmentTimeRepository;
        private readonly IMapper _mapper;

        public GetListAppointmentTimeQueryHandler(IAppointmentTimeRepository appointmentTimeRepository, IMapper mapper)
        {
            _appointmentTimeRepository = appointmentTimeRepository;
            _mapper = mapper;
        }

        public async Task<IList<GetListAppointmentTimeListItemDto>> Handle(GetListAppointmentTimeQuery request, CancellationToken cancellationToken)
        {
            IList<AppointmentTime> appointmentTimes = await _appointmentTimeRepository.GetListWithoutPaginationAsync();
            IList<GetListAppointmentTimeListItemDto> response = _mapper.Map<IList<GetListAppointmentTimeListItemDto>>(appointmentTimes);
            return response;
        }
    }
}

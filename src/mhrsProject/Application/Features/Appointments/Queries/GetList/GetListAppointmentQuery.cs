using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Application.Features.Appointments.Constants.AppointmentOperationClaims;

namespace Application.Features.Appointments.Queries.GetList;

public class GetListAppointmentQuery : IRequest<GetListResponse<GetListAppointmentListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListAppointments({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetAppointments";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListAppointmentQueryHandler : IRequestHandler<GetListAppointmentQuery, GetListResponse<GetListAppointmentListItemDto>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public GetListAppointmentQueryHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListAppointmentListItemDto>> Handle(GetListAppointmentQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Appointment> appointments = await _appointmentRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAppointmentListItemDto> response = _mapper.Map<GetListResponse<GetListAppointmentListItemDto>>(appointments);
            return response;
        }
    }
}

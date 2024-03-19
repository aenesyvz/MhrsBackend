using Application.Features.Appointments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Appointments.Constants.AppointmentOperationClaims;
namespace Application.Features.Appointments.Queries.GetById;

public class GetByIdAppointmentQuery : IRequest<GetByIdAppointmentResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdAppointmentQueryHandler : IRequestHandler<GetByIdAppointmentQuery, GetByIdAppointmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly AppointmentBusinessRules _appointmentBusinessRules;

        public GetByIdAppointmentQueryHandler(IMapper mapper, IAppointmentRepository appointmentRepository, AppointmentBusinessRules appointmentBusinessRules)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _appointmentBusinessRules = appointmentBusinessRules;
        }

        public async Task<GetByIdAppointmentResponse> Handle(GetByIdAppointmentQuery request, CancellationToken cancellationToken)
        {
            Appointment? appointment = await _appointmentRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _appointmentBusinessRules.AppointmentShouldExistWhenSelected(appointment);

            GetByIdAppointmentResponse response = _mapper.Map<GetByIdAppointmentResponse>(appointment);
            return response;
        }
    }
}
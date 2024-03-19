using Application.Features.Appointments.Constants;
using Application.Features.Appointments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using static Application.Features.Appointments.Constants.AppointmentOperationClaims;

namespace Application.Features.Appointments.Commands.Delete;

public class DeleteAppointmentCommand : IRequest<DeletedAppointmentResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, AppointmentOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetAppointments" };



    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, DeletedAppointmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly AppointmentBusinessRules _appointmentBusinessRules;

        public DeleteAppointmentCommandHandler(IMapper mapper, IAppointmentRepository appointmentRepository,
                                         AppointmentBusinessRules appointmentBusinessRules)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _appointmentBusinessRules = appointmentBusinessRules;
        }

        public async Task<DeletedAppointmentResponse> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            Appointment? appointment = await _appointmentRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _appointmentBusinessRules.AppointmentShouldExistWhenSelected(appointment);

            await _appointmentRepository.DeleteAsync(appointment!);

            DeletedAppointmentResponse response = _mapper.Map<DeletedAppointmentResponse>(appointment);
            return response;
        }
    }
}
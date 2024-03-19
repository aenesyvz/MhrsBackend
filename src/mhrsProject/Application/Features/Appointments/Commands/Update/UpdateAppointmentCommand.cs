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

namespace Application.Features.Appointments.Commands.Update;

public class UpdateAppointmentCommand : IRequest<UpdatedAppointmentResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid AppointmentTimeId { get; set; }
    public Guid HospitalId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid PolyclinicId { get; set; }
    public Guid PatientId { get; set; }

    public string[] Roles => new[] { Admin, Write, AppointmentOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetAppointments" };

    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, UpdatedAppointmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly AppointmentBusinessRules _appointmentBusinessRules;

        public UpdateAppointmentCommandHandler(IMapper mapper, IAppointmentRepository appointmentRepository,
                                         AppointmentBusinessRules appointmentBusinessRules)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _appointmentBusinessRules = appointmentBusinessRules;
        }

        public async Task<UpdatedAppointmentResponse> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            Appointment? appointment = await _appointmentRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _appointmentBusinessRules.AppointmentShouldExistWhenSelected(appointment);
            appointment = _mapper.Map(request, appointment);

            await _appointmentRepository.UpdateAsync(appointment!);

            UpdatedAppointmentResponse response = _mapper.Map<UpdatedAppointmentResponse>(appointment);
            return response;
        }
    }
}
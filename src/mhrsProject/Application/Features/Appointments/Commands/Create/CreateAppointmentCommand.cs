using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Transaction;
using MediatR;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Appointments.Constants.AppointmentOperationClaims;
using Application.Features.Appointments.Rules;
using Application.Features.Appointments.Constants;


namespace Application.Features.Appointments.Commands.Create;
public class CreateAppointmentCommand : IRequest<CreatedAppointmentResponse>, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid AppointmentTimeId { get; set; }
    public Guid HospitalId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid PolyclinicId { get; set; }
    public Guid PatientId { get; set; }
    public DateTime Date { get; set; }

    public string[] Roles => new[] { Admin, Write, AppointmentOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetAppointments" };

 

    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, CreatedAppointmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly AppointmentBusinessRules _appointmentBusinessRules;

        public CreateAppointmentCommandHandler(IMapper mapper, IAppointmentRepository appointmentRepository,
                                         AppointmentBusinessRules appointmentBusinessRules)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _appointmentBusinessRules = appointmentBusinessRules;
        }

        public async Task<CreatedAppointmentResponse> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            Appointment appointment = _mapper.Map<Appointment>(request);

            await _appointmentRepository.AddAsync(appointment);

            CreatedAppointmentResponse response = _mapper.Map<CreatedAppointmentResponse>(appointment);
            return response;
        }
    }
}

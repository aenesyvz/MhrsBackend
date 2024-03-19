using Application.Features.AppointmentTimes.Contants;
using Application.Features.AppointmentTimes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.AppointmentTimes.Contants.AppointmentTimesOperationClaims;

namespace Application.Features.AppointmentTimes.Commands.Delete;



public class DeleteAppointmentTimeCommand : IRequest<DeletedAppointmentTimeResponse>/*, ISecuredRequest*/, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, AppointmentTimesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetAppointmentTimes" };

    public class DeleteAppointmentTimeCommandHandler : IRequestHandler<DeleteAppointmentTimeCommand, DeletedAppointmentTimeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentTimeRepository _appointmentTimeRepository;
        private readonly AppointmentTimeBusinessRules _appointmentTimeBusinessRules;

        public DeleteAppointmentTimeCommandHandler(IMapper mapper, IAppointmentTimeRepository appointmentTimeRepository,
                                         AppointmentTimeBusinessRules appointmentTimeBusinessRules)
        {
            _mapper = mapper;
            _appointmentTimeRepository = appointmentTimeRepository;
            _appointmentTimeBusinessRules = appointmentTimeBusinessRules;
        }

        public async Task<DeletedAppointmentTimeResponse> Handle(DeleteAppointmentTimeCommand request, CancellationToken cancellationToken)
        {
            AppointmentTime? appointmentTime = await _appointmentTimeRepository.GetAsync(predicate: at => at.Id == request.Id, cancellationToken: cancellationToken);
            await _appointmentTimeBusinessRules.AppointmentTimeShouldExistWhenSelected(appointmentTime);

            await _appointmentTimeRepository.DeleteAsync(appointmentTime!);

            DeletedAppointmentTimeResponse response = _mapper.Map<DeletedAppointmentTimeResponse>(appointmentTime);
            return response;
        }
    }
}
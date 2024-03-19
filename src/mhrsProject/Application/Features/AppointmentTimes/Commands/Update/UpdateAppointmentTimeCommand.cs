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

namespace Application.Features.AppointmentTimes.Commands.Update;


public class UpdateAppointmentTimeCommand : IRequest<UpdatedAppointmentTimeResponse>, /*ISecuredRequest,*/ ICacheRemoverRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public int Hour { get; set; }
    public int Minute { get; set; }

    public string[] Roles => new[] { Admin, Write, AppointmentTimesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetAppointmentTimes" };

    public class UpdateAppointmentTimeCommandHandler : IRequestHandler<UpdateAppointmentTimeCommand, UpdatedAppointmentTimeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentTimeRepository _appointmentTimeRepository;
        private readonly AppointmentTimeBusinessRules _appointmentTimeBusinessRules;

        public UpdateAppointmentTimeCommandHandler(IMapper mapper, IAppointmentTimeRepository appointmentTimeRepository,
                                         AppointmentTimeBusinessRules appointmentTimeBusinessRules)
        {
            _mapper = mapper;
            _appointmentTimeRepository = appointmentTimeRepository;
            _appointmentTimeBusinessRules = appointmentTimeBusinessRules;
        }

        public async Task<UpdatedAppointmentTimeResponse> Handle(UpdateAppointmentTimeCommand request, CancellationToken cancellationToken)
        {
            AppointmentTime? appointmentTime = await _appointmentTimeRepository.GetAsync(predicate: at => at.Id == request.Id, cancellationToken: cancellationToken);
            await _appointmentTimeBusinessRules.AppointmentTimeShouldExistWhenSelected(appointmentTime);
            appointmentTime = _mapper.Map(request, appointmentTime);

            await _appointmentTimeRepository.UpdateAsync(appointmentTime!);

            UpdatedAppointmentTimeResponse response = _mapper.Map<UpdatedAppointmentTimeResponse>(appointmentTime);
            return response;
        }
    }
}
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

namespace Application.Features.AppointmentTimes.Commands.Create;



public class CreateAppointmentTimeCommand : IRequest<CreatedAppointmentTimeResponse>/*, ISecuredRequest*/, ICacheRemoverRequest, ITransactionalRequest
{
    public int Hour { get; set; }
    public int Minute { get; set; }

    public string[] Roles => new[] { Admin, Write, AppointmentTimesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetAppointmentTimes" };

   

    public class CreateAppointmentTimeCommandHandler : IRequestHandler<CreateAppointmentTimeCommand, CreatedAppointmentTimeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentTimeRepository _appointmentTimeRepository;
        private readonly AppointmentTimeBusinessRules _appointmentTimeBusinessRules;

        public CreateAppointmentTimeCommandHandler(IMapper mapper, IAppointmentTimeRepository appointmentTimeRepository,
                                         AppointmentTimeBusinessRules appointmentTimeBusinessRules)
        {
            _mapper = mapper;
            _appointmentTimeRepository = appointmentTimeRepository;
            _appointmentTimeBusinessRules = appointmentTimeBusinessRules;
        }

        public async Task<CreatedAppointmentTimeResponse> Handle(CreateAppointmentTimeCommand request, CancellationToken cancellationToken)
        {
            await _appointmentTimeBusinessRules.AppointmentTimeCannotBeDuplicateWhenInsertedOrUpdated(request.Hour, request.Minute);

            AppointmentTime appointmentTime = _mapper.Map<AppointmentTime>(request);

            await _appointmentTimeRepository.AddAsync(appointmentTime);
            CreatedAppointmentTimeResponse response = _mapper.Map<CreatedAppointmentTimeResponse>(appointmentTime);

            return response;
        }
    }
}
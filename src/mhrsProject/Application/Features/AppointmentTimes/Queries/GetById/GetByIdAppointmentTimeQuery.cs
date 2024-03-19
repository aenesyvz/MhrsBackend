using Application.Features.AppointmentTimes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.AppointmentTimes.Contants.AppointmentTimesOperationClaims;

namespace Application.Features.AppointmentTimes.Queries.GetById;

public class GetByIdAppointmentTimeQuery : IRequest<GetByIdAppointmentTimeResponse>
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdAppointmentTimeQueryHandler : IRequestHandler<GetByIdAppointmentTimeQuery, GetByIdAppointmentTimeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentTimeRepository _appointmentTimeRepository;
        private readonly AppointmentTimeBusinessRules _appointmentTimeBusinessRules;

        public GetByIdAppointmentTimeQueryHandler(IMapper mapper, IAppointmentTimeRepository appointmentTimeRepository, AppointmentTimeBusinessRules appointmentTimeBusinessRules)
        {
            _mapper = mapper;
            _appointmentTimeRepository = appointmentTimeRepository;
            _appointmentTimeBusinessRules = appointmentTimeBusinessRules;
        }

        public async Task<GetByIdAppointmentTimeResponse> Handle(GetByIdAppointmentTimeQuery request, CancellationToken cancellationToken)
        {
            AppointmentTime? appointmentTime = await _appointmentTimeRepository.GetAsync(predicate: at => at.Id == request.Id, cancellationToken: cancellationToken);
            await _appointmentTimeBusinessRules.AppointmentTimeShouldExistWhenSelected(appointmentTime);

            GetByIdAppointmentTimeResponse response = _mapper.Map<GetByIdAppointmentTimeResponse>(appointmentTime);
            return response;
        }
    }
}

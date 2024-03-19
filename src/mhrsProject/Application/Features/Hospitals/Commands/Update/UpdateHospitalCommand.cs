using Application.Features.Hospitals.Contants;
using Application.Features.Hospitals.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Hospitals.Contants.HospitalsOperationClaims;


namespace Application.Features.Hospitals.Commands.Update;
public class UpdateHospitalCommand : IRequest<UpdatedHospitalResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public HospitalClassType HospitalClassType { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public Guid CityId { get; set; }
    public Guid DistrictId { get; set; }

    public string[] Roles => new[] { Admin, Write, HospitalsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetHospitals" };

    public class UpdateHospitalCommandHandler : IRequestHandler<UpdateHospitalCommand, UpdatedHospitalResponse>
    {
        private readonly IMapper _mapper;
        private readonly IHospitalRepository _hospitalRepository;
        private readonly HospitalBusinessRules _hospitalBusinessRules;

        public UpdateHospitalCommandHandler(IMapper mapper, IHospitalRepository hospitalRepository,
                                         HospitalBusinessRules hospitalBusinessRules)
        {
            _mapper = mapper;
            _hospitalRepository = hospitalRepository;
            _hospitalBusinessRules = hospitalBusinessRules;
        }

        public async Task<UpdatedHospitalResponse> Handle(UpdateHospitalCommand request, CancellationToken cancellationToken)
        {
            Hospital? hospital = await _hospitalRepository.GetAsync(predicate: h => h.Id == request.Id, cancellationToken: cancellationToken);
            await _hospitalBusinessRules.HospitalShouldExistWhenSelected(hospital);
            hospital = _mapper.Map(request, hospital);

            await _hospitalRepository.UpdateAsync(hospital!);

            UpdatedHospitalResponse response = _mapper.Map<UpdatedHospitalResponse>(hospital);
            return response;
        }
    }
}
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
using static Application.Features.Hospitals.Contants.HospitalsOperationClaims;
namespace Application.Features.Hospitals.Commands.Create;

public class CreateHospitalCommand : IRequest<CreatedHospitalResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{

    public string Name { get; set; }
    public HospitalClassType HospitalClassType { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public Guid CityId { get; set; }
    public Guid DistrictId { get; set; }

    public string[] Roles => new[] { Admin, Write, HospitalsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetHospitals" };

    public class CreateHospitalCommandHandler : IRequestHandler<CreateHospitalCommand, CreatedHospitalResponse>
    {
        private readonly IMapper _mapper;
        private readonly IHospitalRepository _hospitalRepository;
        private readonly HospitalBusinessRules _hospitalBusinessRules;

        public CreateHospitalCommandHandler(IMapper mapper, IHospitalRepository hospitalRepository,
                                         HospitalBusinessRules hospitalBusinessRules)
        {
            _mapper = mapper;
            _hospitalRepository = hospitalRepository;
            _hospitalBusinessRules = hospitalBusinessRules;
        }

        public async Task<CreatedHospitalResponse> Handle(CreateHospitalCommand request, CancellationToken cancellationToken)
        {
            Hospital hospital = _mapper.Map<Hospital>(request);

            await _hospitalRepository.AddAsync(hospital);

            CreatedHospitalResponse response = _mapper.Map<CreatedHospitalResponse>(hospital);
            return response;
        }
    }
}
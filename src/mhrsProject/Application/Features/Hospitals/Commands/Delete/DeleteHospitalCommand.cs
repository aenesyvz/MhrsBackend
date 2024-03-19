using Application.Features.Hospitals.Contants;
using Application.Features.Hospitals.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using static Application.Features.Hospitals.Contants.HospitalsOperationClaims;

namespace Application.Features.Hospitals.Commands.Delete;

public class DeleteHospitalCommand : IRequest<DeletedHospitalResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, HospitalsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetHospitals" };

    public class DeleteHospitalCommandHandler : IRequestHandler<DeleteHospitalCommand, DeletedHospitalResponse>
    {
        private readonly IMapper _mapper;
        private readonly IHospitalRepository _hospitalRepository;
        private readonly HospitalBusinessRules _hospitalBusinessRules;

        public DeleteHospitalCommandHandler(IMapper mapper, IHospitalRepository hospitalRepository,
                                         HospitalBusinessRules hospitalBusinessRules)
        {
            _mapper = mapper;
            _hospitalRepository = hospitalRepository;
            _hospitalBusinessRules = hospitalBusinessRules;
        }

        public async Task<DeletedHospitalResponse> Handle(DeleteHospitalCommand request, CancellationToken cancellationToken)
        {
            Hospital? hospital = await _hospitalRepository.GetAsync(predicate: h => h.Id == request.Id, cancellationToken: cancellationToken);
            await _hospitalBusinessRules.HospitalShouldExistWhenSelected(hospital);

            await _hospitalRepository.DeleteAsync(hospital!);

            DeletedHospitalResponse response = _mapper.Map<DeletedHospitalResponse>(hospital);
            return response;
        }
    }
}
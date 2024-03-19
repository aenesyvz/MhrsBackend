using Application.Features.Diseases.Contants;
using Application.Features.Diseases.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using static Application.Features.Diseases.Contants.DiseasesOperationClaims;

namespace Application.Features.Diseases.Commands.Create;

public class CreateDiseaseCommand : IRequest<CreatedDiseaseResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid PolyclinicId { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, DiseasesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetDiseases" };

    public class CreateDiseaseCommandHandler : IRequestHandler<CreateDiseaseCommand, CreatedDiseaseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDiseaseRepository _diseaseRepository;
        private readonly DiseaseBusinessRules _diseaseBusinessRules;

        public CreateDiseaseCommandHandler(IMapper mapper, IDiseaseRepository diseaseRepository,
                                         DiseaseBusinessRules diseaseBusinessRules)
        {
            _mapper = mapper;
            _diseaseRepository = diseaseRepository;
            _diseaseBusinessRules = diseaseBusinessRules;
        }

        public async Task<CreatedDiseaseResponse> Handle(CreateDiseaseCommand request, CancellationToken cancellationToken)
        {
            await _diseaseBusinessRules.DiseaseCannotBeDuplicateWhenInsertedOrUpdated(request.PolyclinicId, request.Name);

            Disease disease = _mapper.Map<Disease>(request);

            await _diseaseRepository.AddAsync(disease);

            CreatedDiseaseResponse response = _mapper.Map<CreatedDiseaseResponse>(disease);
            return response;
        }
    }
}

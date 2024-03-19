using Application.Features.Diseases.Contants;
using Application.Features.Diseases.Rules;
using Application.Services.Repositories;
using AutoMapper;
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
using static Application.Features.Diseases.Contants.DiseasesOperationClaims;

namespace Application.Features.Diseases.Commands.Update;

public class UpdateDiseaseCommand : IRequest<UpdatedDiseaseResponse>, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid PolyclinicId { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, DiseasesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetDiseases" };

    public class UpdateDiseaseCommandHandler : IRequestHandler<UpdateDiseaseCommand, UpdatedDiseaseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDiseaseRepository _diseaseRepository;
        private readonly DiseaseBusinessRules _diseaseBusinessRules;

        public UpdateDiseaseCommandHandler(IMapper mapper, IDiseaseRepository diseaseRepository,
                                         DiseaseBusinessRules diseaseBusinessRules)
        {
            _mapper = mapper;
            _diseaseRepository = diseaseRepository;
            _diseaseBusinessRules = diseaseBusinessRules;
        }

        public async Task<UpdatedDiseaseResponse> Handle(UpdateDiseaseCommand request, CancellationToken cancellationToken)
        {
            Disease? disease = await _diseaseRepository.GetAsync(predicate: d => d.Id == request.Id, cancellationToken: cancellationToken);
            await _diseaseBusinessRules.DiseaseShouldExistWhenSelected(disease);
            await _diseaseBusinessRules.DiseaseCannotBeDuplicateWhenInsertedOrUpdated(request!.PolyclinicId, request!.Name);

            disease = _mapper.Map(request, disease);

            await _diseaseRepository.UpdateAsync(disease!);

            UpdatedDiseaseResponse response = _mapper.Map<UpdatedDiseaseResponse>(disease);
            return response;
        }
    }
}
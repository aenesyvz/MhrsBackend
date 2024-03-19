using Application.Features.Diseases.Contants;
using Application.Features.Diseases.Rules;
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
using static Application.Features.Diseases.Contants.DiseasesOperationClaims;


namespace Application.Features.Diseases.Commands.Delete;

public class DeleteDiseaseCommand : IRequest<DeletedDiseaseResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, DiseasesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey =>  new[] { "GetDiseases" };

    public class DeleteDiseaseCommandHandler : IRequestHandler<DeleteDiseaseCommand, DeletedDiseaseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDiseaseRepository _diseaseRepository;
        private readonly DiseaseBusinessRules _diseaseBusinessRules;

        public DeleteDiseaseCommandHandler(IMapper mapper, IDiseaseRepository diseaseRepository,
                                         DiseaseBusinessRules diseaseBusinessRules)
        {
            _mapper = mapper;
            _diseaseRepository = diseaseRepository;
            _diseaseBusinessRules = diseaseBusinessRules;
        }

        public async Task<DeletedDiseaseResponse> Handle(DeleteDiseaseCommand request, CancellationToken cancellationToken)
        {
            Disease? disease = await _diseaseRepository.GetAsync(predicate: d => d.Id == request.Id, cancellationToken: cancellationToken);
            await _diseaseBusinessRules.DiseaseShouldExistWhenSelected(disease);

            await _diseaseRepository.DeleteAsync(disease!);

            DeletedDiseaseResponse response = _mapper.Map<DeletedDiseaseResponse>(disease);
            return response;
        }
    }
}
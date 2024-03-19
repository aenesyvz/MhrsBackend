using Application.Features.Diseases.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Diseases.Contants.DiseasesOperationClaims;

namespace Application.Features.Diseases.Queries.GetById;
public class GetByIdDiseaseQuery : IRequest<GetByIdDiseaseResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdDiseaseQueryHandler : IRequestHandler<GetByIdDiseaseQuery, GetByIdDiseaseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDiseaseRepository _diseaseRepository;
        private readonly DiseaseBusinessRules _diseaseBusinessRules;

        public GetByIdDiseaseQueryHandler(IMapper mapper, IDiseaseRepository diseaseRepository, DiseaseBusinessRules diseaseBusinessRules)
        {
            _mapper = mapper;
            _diseaseRepository = diseaseRepository;
            _diseaseBusinessRules = diseaseBusinessRules;
        }

        public async Task<GetByIdDiseaseResponse> Handle(GetByIdDiseaseQuery request, CancellationToken cancellationToken)
        {
            Disease? disease = await _diseaseRepository.GetAsync(
                predicate: d => d.Id == request.Id,
                include: d => d.Include(d => d.Polyclinic),
                cancellationToken: cancellationToken
            );

            await _diseaseBusinessRules.DiseaseShouldExistWhenSelected(disease);

            GetByIdDiseaseResponse response = _mapper.Map<GetByIdDiseaseResponse>(disease);
            return response;
        }
    }
}

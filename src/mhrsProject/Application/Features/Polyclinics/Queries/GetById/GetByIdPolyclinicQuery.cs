using Application.Features.Polyclinics.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Polyclinics.Contants.PolyclinicsOperationClaims;

namespace Application.Features.Polyclinics.Queries.GetById;
public class GetByIdPolyclinicQuery : IRequest<GetByIdPolyclinicResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdPolyclinicQueryHandler : IRequestHandler<GetByIdPolyclinicQuery, GetByIdPolyclinicResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPolyclinicRepository _polyclinicRepository;
        private readonly PolyclinicBusinessRules _polyclinicBusinessRules;

        public GetByIdPolyclinicQueryHandler(IMapper mapper, IPolyclinicRepository polyclinicRepository, PolyclinicBusinessRules polyclinicBusinessRules)
        {
            _mapper = mapper;
            _polyclinicRepository = polyclinicRepository;
            _polyclinicBusinessRules = polyclinicBusinessRules;
        }

        public async Task<GetByIdPolyclinicResponse> Handle(GetByIdPolyclinicQuery request, CancellationToken cancellationToken)
        {
            Polyclinic? polyclinic = await _polyclinicRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _polyclinicBusinessRules.PolyclinicShouldExistWhenSelected(polyclinic);

            GetByIdPolyclinicResponse response = _mapper.Map<GetByIdPolyclinicResponse>(polyclinic);
            return response;
        }
    }
}

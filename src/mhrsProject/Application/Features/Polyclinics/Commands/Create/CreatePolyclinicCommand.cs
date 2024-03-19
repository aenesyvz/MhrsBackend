using Application.Features.Polyclinics.Contants;
using Application.Features.Polyclinics.Rules;
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
using static Application.Features.Polyclinics.Contants.PolyclinicsOperationClaims;

namespace Application.Features.Polyclinics.Commands.Create;

public class CreatePolyclinicCommand : IRequest<CreatedPolyclinicResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, PolyclinicsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetPolyclinics" };

    public class CreatePolyclinicCommandHandler : IRequestHandler<CreatePolyclinicCommand, CreatedPolyclinicResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPolyclinicRepository _polyclinicRepository;
        private readonly PolyclinicBusinessRules _polyclinicBusinessRules;

        public CreatePolyclinicCommandHandler(IMapper mapper, IPolyclinicRepository polyclinicRepository,
                                         PolyclinicBusinessRules polyclinicBusinessRules)
        {
            _mapper = mapper;
            _polyclinicRepository = polyclinicRepository;
            _polyclinicBusinessRules = polyclinicBusinessRules;
        }

        public async Task<CreatedPolyclinicResponse> Handle(CreatePolyclinicCommand request, CancellationToken cancellationToken)
        {
            Polyclinic polyclinic = _mapper.Map<Polyclinic>(request);

            await _polyclinicRepository.AddAsync(polyclinic);

            CreatedPolyclinicResponse response = _mapper.Map<CreatedPolyclinicResponse>(polyclinic);
            return response;
        }
    }
}
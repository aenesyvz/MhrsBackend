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

namespace Application.Features.Polyclinics.Commands.Update;

public class UpdatePolyclinicCommand : IRequest<UpdatedPolyclinicResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, PolyclinicsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetPolyclinics" };

    public class UpdatePolyclinicCommandHandler : IRequestHandler<UpdatePolyclinicCommand, UpdatedPolyclinicResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPolyclinicRepository _polyclinicRepository;
        private readonly PolyclinicBusinessRules _polyclinicBusinessRules;

        public UpdatePolyclinicCommandHandler(IMapper mapper, IPolyclinicRepository polyclinicRepository,
                                         PolyclinicBusinessRules polyclinicBusinessRules)
        {
            _mapper = mapper;
            _polyclinicRepository = polyclinicRepository;
            _polyclinicBusinessRules = polyclinicBusinessRules;
        }

        public async Task<UpdatedPolyclinicResponse> Handle(UpdatePolyclinicCommand request, CancellationToken cancellationToken)
        {
            Polyclinic? polyclinic = await _polyclinicRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _polyclinicBusinessRules.PolyclinicShouldExistWhenSelected(polyclinic);
            polyclinic = _mapper.Map(request, polyclinic);

            await _polyclinicRepository.UpdateAsync(polyclinic!);

            UpdatedPolyclinicResponse response = _mapper.Map<UpdatedPolyclinicResponse>(polyclinic);
            return response;
        }
    }
}
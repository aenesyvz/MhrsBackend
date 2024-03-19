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

namespace Application.Features.Polyclinics.Commands.Delete;



public class DeletePolyclinicCommand : IRequest<DeletedPolyclinicResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, PolyclinicsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetPolyclinics" };

    public class DeletePolyclinicCommandHandler : IRequestHandler<DeletePolyclinicCommand, DeletedPolyclinicResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPolyclinicRepository _polyclinicRepository;
        private readonly PolyclinicBusinessRules _polyclinicBusinessRules;

        public DeletePolyclinicCommandHandler(IMapper mapper, IPolyclinicRepository polyclinicRepository,
                                         PolyclinicBusinessRules polyclinicBusinessRules)
        {
            _mapper = mapper;
            _polyclinicRepository = polyclinicRepository;
            _polyclinicBusinessRules = polyclinicBusinessRules;
        }

        public async Task<DeletedPolyclinicResponse> Handle(DeletePolyclinicCommand request, CancellationToken cancellationToken)
        {
            Polyclinic? polyclinic = await _polyclinicRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _polyclinicBusinessRules.PolyclinicShouldExistWhenSelected(polyclinic);

            await _polyclinicRepository.DeleteAsync(polyclinic!);

            DeletedPolyclinicResponse response = _mapper.Map<DeletedPolyclinicResponse>(polyclinic);
            return response;
        }
    }
}
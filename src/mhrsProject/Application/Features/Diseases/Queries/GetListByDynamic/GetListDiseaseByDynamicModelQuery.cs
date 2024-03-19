using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Dynamic;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Diseases.Queries.GetListByDynamic;


public class GetListDiseaseByDynamicModelQuery : IRequest<IList<GetListDiseaseByDynamicModelListItemDto>>
{
    public Guid PolyclinicId { get; set; }
    public class GetListDiseaseByDynamicModelQueryHandler : IRequestHandler<GetListDiseaseByDynamicModelQuery, IList<GetListDiseaseByDynamicModelListItemDto>>
    {
        public readonly IDiseaseRepository _diseaseRepository;
        public readonly IMapper _mapper;

        public GetListDiseaseByDynamicModelQueryHandler(IDiseaseRepository diseaseRepository, IMapper mapper)
        {
            _diseaseRepository = diseaseRepository;
            _mapper = mapper;
        }

        public async Task<IList<GetListDiseaseByDynamicModelListItemDto>> Handle(GetListDiseaseByDynamicModelQuery request, CancellationToken cancellationToken)
        {
            IList<Disease> diseases = await _diseaseRepository.GetListByPolyclinicIdAsync(request.PolyclinicId);

            IList<GetListDiseaseByDynamicModelListItemDto> response = _mapper.Map<IList<GetListDiseaseByDynamicModelListItemDto>>(diseases);

            return response;
        }
    }
}

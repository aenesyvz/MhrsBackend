using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Dynamic;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hospitals.Queries.GetListByDynamic;


public class GetListHospitalByDynamicModelQuery : IRequest<IList<GetListHospitalByDynamicModelListItemDto>>
{
    public DynamicQuery? DynamicQuery { get; set; }

    public class GetListHospitalByDynamicModelQueryHandler : IRequestHandler<GetListHospitalByDynamicModelQuery, IList<GetListHospitalByDynamicModelListItemDto>>
    {
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IMapper _mapper;
        public GetListHospitalByDynamicModelQueryHandler(IHospitalRepository hospitalRepository, IMapper mapper)
        {
            _hospitalRepository = hospitalRepository;
            _mapper = mapper;
        }

        public async Task<IList<GetListHospitalByDynamicModelListItemDto>> Handle(GetListHospitalByDynamicModelQuery request, CancellationToken cancellationToken)
        {
            IList<Hospital> hospitals = await _hospitalRepository.GetListByDynamicWithOutPaginationAsync(
                    dynamic: request.DynamicQuery,
                    include: h => h.Include(h => h.City).Include(h => h.District),
                    cancellationToken: cancellationToken
                );

            IList<GetListHospitalByDynamicModelListItemDto> response = _mapper.Map<IList<GetListHospitalByDynamicModelListItemDto>>(hospitals);

            return response;
        }
    }
}

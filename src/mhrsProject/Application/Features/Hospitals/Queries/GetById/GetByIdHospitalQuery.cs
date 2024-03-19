using Application.Features.Hospitals.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.Hospitals.Contants.HospitalsOperationClaims;

namespace Application.Features.Hospitals.Queries.GetById;

public class GetByIdHospitalQuery : IRequest<GetByIdHospitalResponse>
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdHospitalQueryHandler : IRequestHandler<GetByIdHospitalQuery, GetByIdHospitalResponse>
    {
        private readonly IMapper _mapper;
        private readonly IHospitalRepository _hospitalRepository;
        private readonly HospitalBusinessRules _hospitalBusinessRules;

        public GetByIdHospitalQueryHandler(IMapper mapper, IHospitalRepository hospitalRepository, HospitalBusinessRules hospitalBusinessRules)
        {
            _mapper = mapper;
            _hospitalRepository = hospitalRepository;
            _hospitalBusinessRules = hospitalBusinessRules;
        }

        public async Task<GetByIdHospitalResponse> Handle(GetByIdHospitalQuery request, CancellationToken cancellationToken)
        {
            Hospital? hospital = await _hospitalRepository.GetAsync(
                predicate: h => h.Id == request.Id,
                include: h => h.Include(h => h.City).Include(h => h.District),
                cancellationToken: cancellationToken
            );
            await _hospitalBusinessRules.HospitalShouldExistWhenSelected(hospital);

            GetByIdHospitalResponse response = _mapper.Map<GetByIdHospitalResponse>(hospital);
            return response;
        }
    }
}

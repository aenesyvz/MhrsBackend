using FluentValidation;

namespace Application.Features.Hospitals.Commands.Create;

public class CreateHospitalCommandValidator : AbstractValidator<CreateHospitalCommand>
{
    public CreateHospitalCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.HospitalClassType).NotEmpty();
        RuleFor(c => c.Latitude).NotEmpty();
        RuleFor(c => c.Longitude).NotEmpty();
        RuleFor(c => c.CityId).NotEmpty();
        RuleFor(c => c.DistrictId).NotEmpty();
    }
}

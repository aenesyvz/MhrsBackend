using FluentValidation;

namespace Application.Features.Hospitals.Commands.Update;

public class UpdateHospitalCommandValidator : AbstractValidator<UpdateHospitalCommand>
{
    public UpdateHospitalCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.HospitalClassType).NotEmpty();
        RuleFor(c => c.Latitude).NotEmpty();
        RuleFor(c => c.Longitude).NotEmpty();
        RuleFor(c => c.CityId).NotEmpty();
        RuleFor(c => c.DistrictId).NotEmpty();
    }
}

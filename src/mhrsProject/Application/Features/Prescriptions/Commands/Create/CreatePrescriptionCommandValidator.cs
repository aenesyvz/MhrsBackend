using FluentValidation;

namespace Application.Features.Prescriptions.Commands.Create;

public class CreatePrescriptionCommandValidator : AbstractValidator<CreatePrescriptionCommand>
{
    public CreatePrescriptionCommandValidator()
    {
        RuleFor(c => c.PatientId).NotEmpty();
        RuleFor(c => c.HospitalId).NotEmpty();
        RuleFor(c => c.DoctorId).NotEmpty();
        RuleFor(c => c.PrescriptionType).NotEmpty();
    }
}

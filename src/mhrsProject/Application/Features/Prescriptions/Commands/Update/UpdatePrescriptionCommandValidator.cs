using FluentValidation;

namespace Application.Features.Prescriptions.Commands.Update;

public class UpdatePrescriptionCommandValidator : AbstractValidator<UpdatePrescriptionCommand>
{
    public UpdatePrescriptionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.PatientId).NotEmpty();
        RuleFor(c => c.HospitalId).NotEmpty();
        RuleFor(c => c.DoctorId).NotEmpty();
        RuleFor(c => c.PrescriptionType).NotEmpty();
    }
}

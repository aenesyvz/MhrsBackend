using FluentValidation;

namespace Application.Features.AppointmentTimes.Commands.Create;

public class CreateAppointmentTimeCommandValidator : AbstractValidator<CreateAppointmentTimeCommand>
{
    public CreateAppointmentTimeCommandValidator()
    {
        RuleFor(c => c.Hour).NotEmpty();
        RuleFor(c => c.Minute)
            .Must(beAnInteger)
            .InclusiveBetween(0, 59).WithMessage("Lütfen geçerli bir dakika girin (0-59)");
    }

    private bool beAnInteger(int value)
    {
        return value % 1 == 0;
    }
}

using FluentValidation;

namespace Application.Features.AppointmentTimes.Commands.Delete;

public class DeleteAppointmentTimeCommandValidator : AbstractValidator<DeleteAppointmentTimeCommand>
{
    public DeleteAppointmentTimeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
